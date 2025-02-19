using Models;

namespace Services;


public interface IReciboService
{
    Task<List<Recibo>> GetAllAsync();
    Task<Recibo?> GetByIdAsync(int idRecibo);
    Task <Recibo>AddAsync(Recibo bebida);
    Task <Recibo>UpdateAsync(Recibo bebida);
    Task DeleteAsync(int id);
    public Task InicializarDatosAsync();
}