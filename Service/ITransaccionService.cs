using Models;

namespace Services;


interface ITransaccionService
{
    Task<List<Transaccion>> GetAllAsync();
    Task<Transaccion?> GetByIdAsync(int idTransaccion);
    Task <Transaccion>AddAsync(Transaccion bebida);
    Task <Transaccion>UpdateAsync(Transaccion bebida);
    Task DeleteAsync(int id);
}