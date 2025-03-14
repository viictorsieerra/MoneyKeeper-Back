using Models;
using Microsoft.Data.SqlClient;
using Repositories;
using DTO;
using System.Security.Claims;
namespace Services;

class ReciboService : IReciboService
{
    private readonly IReciboRepository? _repository;

    public ReciboService(IReciboRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Recibo>> GetAllAsync()
    {
        List<Recibo> recibos = await _repository.GetAllAsync();
        return recibos;
    }

    public async Task<Recibo> GetByIdAsync(int idRecibo)
    {
        Recibo? recibo = await _repository.GetByIdAsync(idRecibo);
        if (recibo == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return recibo;
    }


      public async Task<Recibo> AddAsync(Recibo recibo)
    {
       
        await _repository.CreateRecibo(recibo);
        return recibo;
    }

        public async Task<Recibo> CreateRecibo(Recibo recibo)
{
    
    if (string.IsNullOrEmpty(recibo._nombreRecibo))
    {
        throw new ArgumentException("El nombre de la Recibo es obligatorio.");
    }

   
    await _repository.CreateRecibo(recibo);

    
    return recibo;
}

    public async Task<Recibo> UpdateAsync(Recibo updatedRecibo)
    {
        var existingRecibo = await _repository.GetByIdAsync(updatedRecibo._idRecibo);

        if (existingRecibo == null)
        {
            throw new Exception("NO SE HAN ENCONTRADO DATOS");
        }

        existingRecibo._nombreRecibo = updatedRecibo._nombreRecibo;
        existingRecibo._idUsuario = updatedRecibo._idUsuario;
        existingRecibo._idCuenta = updatedRecibo._idCuenta;
        existingRecibo._dineroRecibo = updatedRecibo._dineroRecibo;
        existingRecibo._activa = updatedRecibo._activa;
        existingRecibo._fecRecibo = updatedRecibo._fecRecibo;
        
        

        await _repository.UpdateAsync(existingRecibo);

        return existingRecibo;
    }


    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);

    }

    public async Task InicializarDatosAsync()
    {
        await _repository.InicializarDatosAsync();
    }
public async Task<List<ReciboDTO>> GetByUser(ClaimsPrincipal user)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return new List<ReciboDTO>();
        }

        string idUsuario = idClaim.Value;

        List<ReciboDTO> recibos = await _repository.GetByUser(idUsuario);
        return recibos;
    }

    
}