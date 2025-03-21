using DTO;
using Models;
namespace Repositories;

public interface ITransaccionRepository
{
    Task<List<Transaccion>> GetAllAsync();
    Task<Transaccion?> GetByIdAsync(int id);
    Task<List<TransaccionDTO>> GetByUser(string id);
    Task<List<TransaccionDTO>>  GetByUserFilter(string idUsuario, string fechaInicio, string fechaFin, int idCategoria);
    Task AddAsync(Transaccion transaccion);
    Task UpdateAsync(Transaccion transaccion);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}