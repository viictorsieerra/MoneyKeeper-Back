using System.Security.Claims;
using Models;
namespace Services;


public interface ITransaccionService
{
    Task<List<Transaccion>> GetAllAsync();
    Task<Transaccion?> GetByIdAsync(int idTransaccion);
     Task<List<Transaccion>> GetByUser(ClaimsPrincipal user);
    Task <Transaccion>AddAsync(Transaccion bebida);
    Task <Transaccion>UpdateAsync(Transaccion bebida);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}