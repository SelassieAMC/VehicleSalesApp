using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleSales.Domain.Interfaces;

public interface IRepository<T>
{
    public IEnumerable<T> GetAll();

    public Task AddAsync(List<T> entities);
}