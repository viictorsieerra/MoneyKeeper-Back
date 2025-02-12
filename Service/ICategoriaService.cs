using Models;

namespace Services;


public interface ICategoriaService
{
    Task<List<Categoria>> GetAllAsync();
    Task<Categoria?> GetByIdAsync(int idCategoria);
    Task <Categoria>AddAsync(Categoria bebida);
    Task <Categoria>UpdateAsync(Categoria bebida);
    Task DeleteAsync(int id);
}