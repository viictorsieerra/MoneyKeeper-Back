using DTO;
using Models;
namespace Repositories;

public interface IMetaAhorroRepository
{
    Task<List<MetaAhorro>> GetAllAsync();
    Task<MetaAhorro?> GetByIdAsync(int id);
    Task AddAsync(MetaAhorro meta);
    Task<List<MetaAhorroDTO>> GetByUser(string id);
    Task UpdateAsync(MetaAhorro meta);
    Task DeleteAsync(int id);
    Task InicializarDatosAsync();
}