using Models;

namespace Services;


public interface IUsuarioService
{
    Task<List<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int idUsuario);
    Task <Usuario>AddAsync(Usuario bebida);
    Task <Usuario>UpdateAsync(Usuario bebida);
    Task DeleteAsync(int id);
}