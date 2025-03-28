using System.Security.Claims;
using DTO;
using Models;

namespace Services;


public interface IPresupuestoService
{
    Task<List<Presupuesto>> GetAllAsync();
    Task<Presupuesto?> GetByIdAsync(int idPresupuesto);
    Task<Presupuesto> AddAsync(Presupuesto presupuesto);
    Task<Presupuesto> UpdateAsync(Presupuesto presupuesto);
    Task<List<PresupuestoDTO>> GetByUser(ClaimsPrincipal user);
    Task DeleteAsync(int idGasto);  
}
