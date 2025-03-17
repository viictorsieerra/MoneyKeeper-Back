using System.Security.Claims;
using DTO;
using Models;

namespace Services;


public interface IMetaAhorroService
{
    Task<List<MetaAhorro>> GetAllAsync();
    Task<MetaAhorro?> GetByIdAsync(int idMetaAhorro);
    Task<MetaAhorro> AddAsync(MetaAhorro meta);
    Task<MetaAhorro> UpdateAsync(MetaAhorro meta);
    Task<List<MetaAhorro>> GetByUser(ClaimsPrincipal user);
    Task DeleteAsync(int idMeta);  
    Task InicializarDatosAsync();
}
