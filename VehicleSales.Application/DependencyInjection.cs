using System;
using Microsoft.Extensions.DependencyInjection;
using VehicleSales.Application.Interfaces;
using VehicleSales.Application.Services;

namespace VehicleSales.Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVehicleSalesDataService, VehicleSalesDataService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}