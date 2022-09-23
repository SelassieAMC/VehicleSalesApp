using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleSales.Domain.Interfaces;
using VehicleSales.Infrastructure.Repositories;

namespace VehicleSales.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IVehicleSaleRepository, VehicleSaleRepository>();
            services.AddDbContext<ApplicationDbContext>(x => x.UseInMemoryDatabase(connectionString));
        }
    }
}