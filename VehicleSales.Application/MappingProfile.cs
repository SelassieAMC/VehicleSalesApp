using AutoMapper;
using VehicleSales.Application.Dtos;
using VehicleSales.Domain.Models;

namespace VehicleSales.Application;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VehicleSale, VehiculeSaleDto>().ReverseMap();
    }
}