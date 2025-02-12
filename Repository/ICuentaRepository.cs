using Models;
namespace Repositories;

public interface ICuentaRepository
{
    Task<List<Cuenta>> GetAllAsync();
    Task<Cuenta?> GetByIdAsync(int id);
    Task AddAsync(Cuenta cuenta);
    Task UpdateAsync(Cuenta cuenta);
    Task DeleteAsync(int id);
}