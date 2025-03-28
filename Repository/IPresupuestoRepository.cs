using DTO;
using Models;
namespace Repositories;

public interface IPresupuestoRepository
{
    Task<List<Presupuesto>> GetAllAsync();
    Task<Presupuesto?> GetByIdAsync(int id);
    Task<List<PresupuestoDTO>> GetByUser(string id);
    Task AddAsync(Presupuesto presupuesto);
    Task UpdateAsync(Presupuesto presupuesto);
    Task DeleteAsync(int id);
}