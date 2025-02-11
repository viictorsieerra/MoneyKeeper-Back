using Models;

namespace Services;


interface ICuentaService
{
    Task<List<Cuenta>> GetAllAsync();
    Task<Cuenta?> GetByIdAsync(int idCuenta);
    Task <Cuenta>AddAsync(Cuenta bebida);
    Task <Cuenta>UpdateAsync(Cuenta bebida);
    Task DeleteAsync(int id);
}