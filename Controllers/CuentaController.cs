using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

// [Authorize (Roles = "Cliente")]
[ApiController]
[Route("[controller]")]
public class CuentaController : ControllerBase
{

    private readonly ICuentaService _service;

    public CuentaController(ICuentaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Cuenta>>> GetCuentas()
    {
        List<Cuenta> cuentas = await _service.GetAllAsync();
        return Ok(cuentas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cuenta>> GetCuenta(int id)
    {
        Cuenta cuenta = await _service.GetByIdAsync(id);
        if (cuenta == null)
        {
            return NotFound();
        }
        return Ok(cuenta);
    }

   [HttpPost]
public async Task<ActionResult<Cuenta>> CreateCuenta(Cuenta cuenta)
{
    
    var nuevaCuenta = await _service.CreateCuenta(cuenta);

   
    return CreatedAtAction(nameof(GetCuenta), new { id = nuevaCuenta._idCuenta }, nuevaCuenta);
}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCuenta(int id, Cuenta updatedCuenta)
    {
        var existingCuenta = await _service.GetByIdAsync(id);
        if (existingCuenta == null)
        {
            return NotFound();
        }

        await _service.UpdateCuenta(updatedCuenta);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCuenta(int id)
    {
        await _service.DeleteAsyncById(id);
        return NoContent();
    }


    [HttpPost("InicializarDatos")]
    public async Task<IActionResult> InicializarDatos()
    {
        await _service.InicializarDatosAsync();
        return Ok();
    }
 [HttpGet("cuentas")]
    public async Task<ActionResult<List<CuentaDTO>>> GetMisCuentas()
    {
        var cuentas = await _service.GetByUser(User);

        if (cuentas == null || cuentas.Count == 0)
        {
            return NotFound("No se encontraron cuentas para este usuario.");
        }

        return Ok(cuentas);
    }
}
