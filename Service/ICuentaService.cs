using Models;

namespace Services;


public interface ICuentaService
{
    Task<List<Cuenta>> GetAllAsync();
    Task<Cuenta?> GetByIdAsync(int idCuenta);
    Task <Cuenta>AddAsync(Cuenta cuenta);
    Task <Cuenta>UpdateAsync(Cuenta cuenta);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}