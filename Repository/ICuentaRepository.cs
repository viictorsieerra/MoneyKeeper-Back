using DTO;
using Models;
namespace Repositories;

public interface ICuentaRepository
{
    Task<List<Cuenta>> GetAllAsync();
    Task<Cuenta?> GetByIdAsync(int id);

     Task<List<CuentaDTO>> GetByUser(string id);
    Task UpdateCuenta(Cuenta cuenta);
   Task<Cuenta> CreateCuenta(Cuenta cuenta);
    Task DeleteAsyncById(int id);
    Task InicializarDatosAsync();
}