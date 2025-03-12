using Models;
using Microsoft.Data.SqlClient;
using Repositories;
using System.Security.Claims;
using DTO;
namespace Services;

class TransaccionService : ITransaccionService
{
    private readonly ITransaccionRepository? _repository;

    public TransaccionService(ITransaccionRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Transaccion>> GetAllAsync()
    {
        List<Transaccion> transaccions = await _repository.GetAllAsync();
        return transaccions;
    }

    public async Task<Transaccion> GetByIdAsync(int idTransaccion)
    {
        Transaccion? transaccion = await _repository.GetByIdAsync(idTransaccion);
        if (transaccion == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return transaccion;
    }

    public async Task<List<TransaccionDTO>> GetByUser(ClaimsPrincipal user)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return new List<TransaccionDTO>();
        }

        string idUsuario = idClaim.Value;

        List<TransaccionDTO> transacciones = await _repository.GetByUser(idUsuario);
        return transacciones;
    }

        public async Task<List<TransaccionDTO>> GetByUserFilter(ClaimsPrincipal user, string fechaInicio, string fechaFin)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return new List<TransaccionDTO>();
        }

        string idUsuario = idClaim.Value;

        List<TransaccionDTO> transacciones = await _repository.GetByUserFilter(idUsuario, fechaInicio, fechaFin);
        return transacciones;
    }

    public async Task<Transaccion> AddAsync(Transaccion transaccion)
    {
        await _repository.AddAsync(transaccion);
        return transaccion;
    }


    public async Task<Transaccion> UpdateAsync(Transaccion updatedTransaccion)
    {
        var existingTransaccion = await _repository.GetByIdAsync(updatedTransaccion._idTransaccion);

        if (existingTransaccion == null)
        {
            throw new Exception("NO SE HAN ENCONTRADO DATOS");
        }

        // Actualizar cuenta
        existingTransaccion._idUsuario = updatedTransaccion._idUsuario;
        existingTransaccion._idCategoria = updatedTransaccion._idCategoria;
         existingTransaccion._idCuenta = updatedTransaccion._idCuenta;
        existingTransaccion._cantidad = updatedTransaccion._cantidad;
        existingTransaccion._descripcionTransaccion = updatedTransaccion._descripcionTransaccion;
        existingTransaccion._fecTransaccion = updatedTransaccion._fecTransaccion;
        existingTransaccion._tipoMovimiento = updatedTransaccion._tipoMovimiento;
        
        

        await _repository.UpdateAsync(existingTransaccion);

        return existingTransaccion;
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