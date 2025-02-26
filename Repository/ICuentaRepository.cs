using DTO;
using Models;
namespace Repositories;

public interface ICuentaRepository
{
    Task<List<Cuenta>> GetAllAsync();
    Task<Cuenta?> GetByIdAsync(int id);
    Task AddAsync(Cuenta cuenta);
     Task<List<CuentaDTO>> GetByUser(string id);
    Task UpdateAsync(Cuenta cuenta);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}