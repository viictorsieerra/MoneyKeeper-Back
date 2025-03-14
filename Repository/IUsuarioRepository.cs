using DTO;
using Models;
namespace Repositories;

public interface IUsuarioRepository
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
    Task<UsuarioDTOOut> RegisterUser(Usuario usuario);
    Task<UsuarioDTOOut> GetUserFromCredentials(LoginDTO loginDTO);
}