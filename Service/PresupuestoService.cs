using Models;
using Repositories;
using DTO;
using System.Security.Claims;

namespace Services;

public class PresupuestoService : IPresupuestoService
{
    private readonly IPresupuestoRepository? _repository;

    public PresupuestoService(IPresupuestoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Presupuesto>> GetAllAsync()
    {
        List<Presupuesto> presupuestos = await _repository.GetAllAsync();
        return presupuestos;
    }

    public async Task<Presupuesto> GetByIdAsync(int idPresupuesto)
    {
        Presupuesto? presupuesto = await _repository.GetByIdAsync(idPresupuesto);
        if (presupuesto == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return presupuesto;
    }
    public async Task<List<PresupuestoDTO>> GetByUser(ClaimsPrincipal user)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return new List<PresupuestoDTO>();
        }

        string idUsuario = idClaim.Value;

        List<PresupuestoDTO> presupuestos = await _repository.GetByUser(idUsuario);
        return presupuestos;
    }

    public async Task<Presupuesto> AddAsync(Presupuesto presupuesto)
    {
        await _repository.AddAsync(presupuesto);
        return presupuesto;
    }

    public async Task<Presupuesto> UpdateAsync(Presupuesto updatedPresupuesto)
    {
        var existingPresupuesto = await _repository.GetByIdAsync((int)updatedPresupuesto._idPresupuesto);

        if (existingPresupuesto == null)
        {
            throw new Exception("No se han encontrado datos");
        }

        existingPresupuesto._idUsuario = updatedPresupuesto._idUsuario;
        existingPresupuesto._idCategoria = updatedPresupuesto._idCategoria;
        existingPresupuesto._nombre = updatedPresupuesto._nombre;
        existingPresupuesto._limite = updatedPresupuesto._limite;
        existingPresupuesto._dineroActual = updatedPresupuesto._dineroActual;
        existingPresupuesto._fecCreacion = updatedPresupuesto._fecCreacion;
        existingPresupuesto._activo = updatedPresupuesto._activo;

        await _repository.UpdateAsync(existingPresupuesto);

        return existingPresupuesto;
    }

    public async Task DeleteAsync(int idPresupuesto)
    {
        await _repository.DeleteAsync(idPresupuesto);
    }
}
