using Models;
using Microsoft.Data.SqlClient;
using Repositories;
namespace Services;

class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository? _repository;

    public CategoriaService(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        List<Categoria> categorias = await _repository.GetAllAsync();
        return categorias;
    }

    public async Task<Categoria> GetByIdAsync(int idCategoria)
    {
        Categoria? categoria = await _repository.GetByIdAsync(idCategoria);
        if (categoria == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return categoria;
    }


    public async Task<Categoria> AddAsync(Categoria categoria)
    {
        await _repository.AddAsync(categoria);
        return categoria;
    }


    public async Task<Categoria> UpdateAsync(Categoria updatedCategoria)
    {
        var existingCategoria = await _repository.GetByIdAsync(updatedCategoria._idCategoria);

        if (existingCategoria == null)
        {
            throw new Exception("NO SE HAN ENCONTRADO DATOS");
        }

        // Actualizar cuenta
        existingCategoria._nombre = updatedCategoria._nombre;
        existingCategoria._descripcion = updatedCategoria._descripcion;
        
        
        await _repository.UpdateAsync(existingCategoria);

        return existingCategoria;
    }


    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);

    }

    public async Task InicializarDatosAsync()
    {
        await _repository.InicializarDatosAsync();
    }


}