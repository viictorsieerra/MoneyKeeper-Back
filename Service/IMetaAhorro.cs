using Models;

namespace Services;


public interface IMetaAhorroService
{
    Task<List<MetaAhorro>> GetAllAsync();
    Task<MetaAhorro?> GetByIdAsync(int idMetaAhorro);
    Task <MetaAhorro>AddAsync(MetaAhorro bebida);
    Task <MetaAhorro>UpdateAsync(MetaAhorro bebida);
    Task DeleteAsync(int id);
}