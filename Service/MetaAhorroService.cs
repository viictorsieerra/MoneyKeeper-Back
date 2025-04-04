using Models;
using Microsoft.Data.SqlClient;
using Repositories;
using DTO;
using System.Security.Claims;
namespace Services;

public class MetaAhorroService : IMetaAhorroService
{
    private readonly IMetaAhorroRepository? _repository;

    public MetaAhorroService(IMetaAhorroRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<MetaAhorro>> GetAllAsync()
    {
        List<MetaAhorro> metas = await _repository.GetAllAsync();
        return metas;
    }

    public async Task<MetaAhorro> GetByIdAsync(int idMetaAhorro)
    {
        MetaAhorro? meta = await _repository.GetByIdAsync(idMetaAhorro);
        if (meta == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return meta;
    }


  public async Task<MetaAhorro> AddAsync(MetaAhorro meta)
{
    await _repository.AddAsync(meta);
    return meta;
}


    public async Task<MetaAhorro> UpdateAsync(MetaAhorro updatedMetaAhorro)
    {
        var existingMetaAhorro = await _repository.GetByIdAsync(updatedMetaAhorro._idMeta);

        if (existingMetaAhorro == null)
        {
            throw new Exception("NO SE HAN ENCONTRADO DATOS");
        }

        // Actualizar MetaAhorro
        existingMetaAhorro._idUsuario = updatedMetaAhorro._idUsuario;
        existingMetaAhorro._nombreMeta = updatedMetaAhorro._nombreMeta;
        existingMetaAhorro._descripcionMeta = updatedMetaAhorro._descripcionMeta;
        existingMetaAhorro._dineroObjetivo = updatedMetaAhorro._dineroObjetivo;
        existingMetaAhorro._dineroActual = updatedMetaAhorro._dineroActual;
        existingMetaAhorro._activoMeta = updatedMetaAhorro._activoMeta;
        existingMetaAhorro._fechaCreacionMeta = updatedMetaAhorro._fechaCreacionMeta;
        existingMetaAhorro._fechaObjetivoMeta = updatedMetaAhorro._fechaObjetivoMeta;

        if (existingMetaAhorro._dineroActual > existingMetaAhorro._dineroObjetivo)
        {
            existingMetaAhorro._activoMeta = false;
        }
        await _repository.UpdateAsync(existingMetaAhorro);

        return existingMetaAhorro;
    }


   public async Task DeleteAsync(int idMeta)
    {
        await _repository.DeleteAsync(idMeta);  
    }
    public async Task InicializarDatosAsync()
    {
        await _repository.InicializarDatosAsync();
    }

    public async Task<List<MetaAhorro>> GetByUser(ClaimsPrincipal user)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return new List<MetaAhorro>();
        }

        string idUsuario = idClaim.Value;

        List<MetaAhorro> metas = await _repository.GetByUser(idUsuario);
        return metas;
    }

    
}