using System.Security.Claims;
using DTO;
using Models;

namespace Services;


public interface IReciboService
{
    Task<List<Recibo>> GetAllAsync();
    Task<Recibo?> GetByIdAsync(int idRecibo);
    Task <Recibo>AddAsync(Recibo bebida);
    Task <Recibo>CreateRecibo(Recibo bebida);

    Task <Recibo>UpdateAsync(Recibo bebida);
      Task<List<ReciboDTO>> GetByUser(ClaimsPrincipal user);
    Task DeleteAsync(int id);
    public Task InicializarDatosAsync();
}