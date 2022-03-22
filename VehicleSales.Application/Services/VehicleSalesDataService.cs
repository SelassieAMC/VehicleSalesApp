using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using VehicleSales.Application.Dtos;
using VehicleSales.Application.Interfaces;
using VehicleSales.Application.Resources;
using VehicleSales.Domain.Models;
using VehicleSales.Domain.Interfaces;

namespace VehicleSales.Application.Services;

public class VehicleSalesDataService : IVehicleSalesDataService
{
    private readonly IVehicleSaleRepository _repository;
    private readonly IMapper _mapper;

    public VehicleSalesDataService(IVehicleSaleRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<string>> UploadVehicleSalesFromCsvAsync(IFormFile formFile)
    {
        FileInfo fi = new FileInfo(formFile.FileName);
        if (fi.Extension != ".csv")
            throw new FormatException($"The file format {fi.Extension} is invalid for the request.");
        
        await using var fileStream = new MemoryStream();
        await formFile.CopyToAsync(fileStream);
        fileStream.Seek(0, SeekOrigin.Begin);

        TextFieldParser reader = new(fileStream, Encoding.Latin1)
        {
            TextFieldType = FieldType.Delimited
        };
        reader.SetDelimiters(",");
        
        var errors = GetSalesVehicleDataFromCsvFile(reader, out List<VehicleSale> salesData);

        await _repository.AddAsync(salesData);

        return errors;
    }

    public IEnumerable<VehiculeSaleDto> GetVehicleSalesData()
    {
        var data = _repository.GetAll();
        return _mapper.Map<IEnumerable<VehicleSale>, IEnumerable<VehiculeSaleDto>>(data);
    }

    private static HashSet<string>? GetSalesVehicleDataFromCsvFile(TextFieldParser reader, out List<VehicleSale>? salesData)
    {
        var errors = new HashSet<string>();
        var index = 0;
        var headers = new string[6];
        salesData = new List<VehicleSale>();

        while (!reader.EndOfData)
        {
            var line = reader.ReadFields();

            if (index == 0)
            {
                headers = line;
                index++;
                continue;
            }
            index++;

            var rowErrors = new HashSet<string>();
            if (!int.TryParse(line?[0], out var dealNumber))
                AddRowError(rowErrors, index, headers?[0]);

            if (string.IsNullOrWhiteSpace(line?[1]))
                AddRowError(rowErrors, index, headers?[1]);

            if (string.IsNullOrWhiteSpace(line?[2]))
                AddRowError(rowErrors, index, headers?[2]);

            if (string.IsNullOrWhiteSpace(line?[3]))
                AddRowError(rowErrors, index, headers?[3]);

            if (!double.TryParse(line?[4], out var price))
                AddRowError(rowErrors, index, headers?[4]);

            if (!DateTime.TryParse(line?[5], out var date))
                AddRowError(rowErrors, index, headers?[5]);

            if (rowErrors.Any())
            {
                errors = errors.Concat(rowErrors).ToHashSet();
                continue;
            }

            var vehicleSaleData = new VehicleSale()
            {
                DealNumber = dealNumber,
                CustomerName = line?[1] ?? "",
                DealerShipName = line?[2] ?? "",
                Vehicle = line?[3] ?? "",
                Price = price,
                Date = date
            };
            salesData.Add(vehicleSaleData);
        }

        return errors;
    }

    private static void AddRowError(HashSet<string>? rowErrors, int position, string header)
    {
        rowErrors?.Add(string.Format(LogStrings.ErrorCsvLine, position, header));
    }
}