using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using VehicleSales.Application.Dtos;

namespace VehicleSales.Application.Interfaces
{
    public interface IVehicleSalesDataService
    {
        public Task<IEnumerable<string>> UploadVehicleSalesFromCsvAsync(IFormFile formFile);
        public IEnumerable<VehiculeSaleDto> GetVehicleSalesData();
    }
}