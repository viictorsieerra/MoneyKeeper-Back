using System.Security.Claims;
using Models;

namespace Services;


public interface IUsuarioService
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int idUsuario);
    Task<Usuario?> GetByToken(ClaimsPrincipal user);
    Task <Usuario>AddAsync(Usuario bebida);
    Task <Usuario>UpdateAsync(Usuario bebida);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}