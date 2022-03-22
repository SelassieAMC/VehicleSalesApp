using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using VehicleSales.Domain.Interfaces;
using VehicleSales.Domain.Models;

namespace VehicleSales.Infrastructure.Repositories;
public class VehicleSaleRepository : IVehicleSaleRepository
{
    private readonly ApplicationDbContext _context;

    public VehicleSaleRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<VehicleSale> GetAll()
    {
        return _context.VehicleSales;
    }

    public async Task AddAsync(List<VehicleSale> entities)
    {
        _context.VehicleSales.AddRange(entities);
        await _context.BulkSaveChangesAsync();
    }
}