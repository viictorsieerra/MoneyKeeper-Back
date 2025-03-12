using DTO;
using Models;
namespace Repositories;

interface IReciboRepository
{
    Task<List<Recibo>> GetAllAsync();
    Task<Recibo?> GetByIdAsync(int id);
    Task AddAsync(Recibo recibo);
    Task <Recibo>CreateRecibo(Recibo recibo);
    Task<List<ReciboDTO>> GetByUser(string id);
    Task UpdateAsync(Recibo recibo);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}