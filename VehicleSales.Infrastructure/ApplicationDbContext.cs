using Microsoft.EntityFrameworkCore;
using VehicleSales.Domain.Models;

namespace VehicleSales.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    { }

    public DbSet<VehicleSale> VehicleSales { get; set; }
}