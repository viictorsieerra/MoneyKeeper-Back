using System.Security.Claims;
using DTO;
using Models;

namespace Services;


public interface IGastoService
{
    Task<List<Gasto>> GetAllAsync();
    Task<Gasto?> GetByIdAsync(int idGasto);
    Task<Gasto> AddAsync(Gasto gasto);
    Task<Gasto> UpdateAsync(Gasto gasto);
    Task<List<Gasto>> GetByPresupuestoAsync(int idPresupuesto);
    Task DeleteAsync(int idGasto);  
}
