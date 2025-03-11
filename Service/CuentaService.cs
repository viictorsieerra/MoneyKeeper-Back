using Models;
using Microsoft.Data.SqlClient;
using Repositories;
using System.Security.Claims;
using DTO;
namespace Services;

public class CuentaService : ICuentaService
{
    private readonly ICuentaRepository _repository;

    public CuentaService(ICuentaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Cuenta>> GetAllAsync()
    {
        List<Cuenta> cuentas = await _repository.GetAllAsync();
        return cuentas;
    }

    public async Task<Cuenta> GetByIdAsync(int idCuenta)
    {
        Cuenta? cuenta = await _repository.GetByIdAsync(idCuenta);
        if (cuenta == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return cuenta;
    }


     public async Task<Cuenta> AddAsync(Cuenta cuenta)
    {
        // Aquí llamamos a CreateCuenta de la interfaz
        await _repository.CreateCuenta(cuenta);
        return cuenta;
    }



    public async Task<Cuenta> UpdateCuenta(Cuenta updatedCuenta)
{
    try
    {
        var existingCuenta = await _repository.GetByIdAsync(updatedCuenta._idCuenta);

        if (existingCuenta == null)
        {
            throw new Exception("No se ha encontrado la cuenta a actualizar");
        }

        // Actualizar la cuenta
        existingCuenta._idUsuario = updatedCuenta._idUsuario;
        existingCuenta._dineroCuenta = updatedCuenta._dineroCuenta;
        existingCuenta._activa = updatedCuenta._activa;
        existingCuenta._fechaCreacion = updatedCuenta._fechaCreacion;
        existingCuenta._nombreCuenta = updatedCuenta._nombreCuenta;

        await _repository.UpdateCuenta(existingCuenta);
        return existingCuenta;
    }
    catch (Exception ex)
    {
        // Aquí puedes registrar el error o lanzarlo de nuevo
        throw new Exception("Error al actualizar la cuenta: " + ex.Message);
    }
}

    
    public async Task DeleteAsyncById(int idCuenta)
    {
        await _repository.DeleteAsyncById(idCuenta);
    }


    public async Task InicializarDatosAsync()
    {
        await _repository.InicializarDatosAsync();
    }


    public async Task<List<CuentaDTO>> GetByUser(ClaimsPrincipal user)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type ==ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return new List<CuentaDTO>();
        }

        string idUsuario = idClaim.Value;

        List<CuentaDTO> cuentas = await _repository.GetByUser(idUsuario);
        return cuentas;
    }

    public async Task<Cuenta> CreateCuenta(Cuenta cuenta)
{
    
    if (string.IsNullOrEmpty(cuenta._nombreCuenta))
    {
        throw new ArgumentException("El nombre de la cuenta es obligatorio.");
    }

   
    await _repository.CreateCuenta(cuenta);

    
    return cuenta;
}

}