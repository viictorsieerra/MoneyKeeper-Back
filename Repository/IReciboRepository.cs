using Models;
namespace Repositories;

interface IReciboRepository
{
    Task<List<Recibo>> GetAllAsync();
    Task<Recibo?> GetByIdAsync(int id);
    Task AddAsync(Recibo recibo);
    Task UpdateAsync(Recibo recibo);
    Task DeleteAsync(int id);
}