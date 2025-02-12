using Models;
namespace Repositories;

interface IMetaAhorroRepository
{
    Task<List<MetaAhorro>> GetAllAsync();
    Task<MetaAhorro?> GetByIdAsync(int id);
    Task AddAsync(MetaAhorro meta);
    Task UpdateAsync(MetaAhorro meta);
    Task DeleteAsync(int id);
}