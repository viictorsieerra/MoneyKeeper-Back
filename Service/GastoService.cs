using Models;
using Repositories;
using DTO;
using System.Security.Claims;

namespace Services;

public class GastoService : IGastoService
{
    private readonly IGastoRepository? _repository;

    public GastoService(IGastoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Gasto>> GetAllAsync()
    {
        List<Gasto> gastos = await _repository.GetAllAsync();
        return gastos;
    }

    public async Task<Gasto> GetByIdAsync(int idGasto)
    {
        Gasto? gasto = await _repository.GetByIdAsync(idGasto);
        if (gasto == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return gasto;
    }

    public async Task<Gasto> AddAsync(Gasto gasto)
    {
        await _repository.AddAsync(gasto);
        return gasto;
    }

    public async Task<Gasto> UpdateAsync(Gasto updatedGasto)
    {

        var existingGasto = await _repository.GetByIdAsync((int)updatedGasto._idGasto);

        if (existingGasto == null)
        {
            throw new Exception("No se han encontrado datos");
        }

        existingGasto._idPresupuesto = updatedGasto._idPresupuesto;
        existingGasto._nombre = updatedGasto._nombre;
        existingGasto._descripcion = updatedGasto._descripcion;
        existingGasto._cantidad = updatedGasto._cantidad;
        existingGasto._fecCreacion = updatedGasto._fecCreacion;

        await _repository.UpdateAsync(existingGasto);

        return existingGasto;
    }

    public async Task DeleteAsync(int idGasto)
    {
        await _repository.DeleteAsync(idGasto);
    }

    public async Task<List<Gasto>> GetByPresupuestoAsync(int idPresupuesto)
    {
        List<Gasto> gastos = await _repository.GetByPresupuestoAsync(idPresupuesto);
        return gastos;
    }
}
