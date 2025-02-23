using Models;
namespace Repositories;

public interface ITransaccionRepository
{
    Task<List<Transaccion>> GetAllAsync();
    Task<Transaccion?> GetByIdAsync(int id);
    Task<List<Transaccion>> GetByUser(string correo);
    Task AddAsync(Transaccion transaccion);
    Task UpdateAsync(Transaccion transaccion);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}