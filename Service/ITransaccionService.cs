using System.Security.Claims;
using DTO;
using Models;
namespace Services;


public interface ITransaccionService
{
    Task<List<Transaccion>> GetAllAsync();
    Task<Transaccion?> GetByIdAsync(int idTransaccion);
    Task<List<TransaccionDTO>> GetByUser(ClaimsPrincipal user);
    Task<List<TransaccionDTO>> GetByUserFilter(ClaimsPrincipal user, string fechaInicio, string fechaFin);
    Task<Transaccion> AddAsync(Transaccion bebida);
    Task<Transaccion> UpdateAsync(Transaccion bebida);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}