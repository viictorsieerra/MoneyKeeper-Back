using Models;

namespace Services;


interface IReciboService
{
    Task<List<Recibo>> GetAllAsync();
    Task<Recibo?> GetByIdAsync(int idRecibo);
    Task <Recibo>AddAsync(Recibo bebida);
    Task <Recibo>UpdateAsync(Recibo bebida);
    Task DeleteAsync(int id);
}