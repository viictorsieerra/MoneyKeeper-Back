using DTO;
using Models;
namespace Repositories;

public interface IGastoRepository
{
    Task<List<Gasto>> GetAllAsync();
    Task<Gasto?> GetByIdAsync(int id);
    Task<List<Gasto>> GetByPresupuestoAsync(int idPresupuesto);
    Task AddAsync(Gasto gasto);
    Task UpdateAsync(Gasto gasto);
    Task DeleteAsync(int id);
}