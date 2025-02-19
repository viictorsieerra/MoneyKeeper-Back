using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

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
        await _service.AddAsync(cuenta);
        return CreatedAtAction(nameof(GetCuenta), new { id = cuenta._idCuenta }, cuenta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCuenta(int idCuenta, Cuenta updatedCuenta)
    {
        var existingCuenta = await _service.GetByIdAsync(idCuenta);
        if (existingCuenta == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedCuenta);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCuenta(int idCuenta)
    {
        await _service.DeleteAsync(idCuenta);
        return NoContent();
    }

    [HttpPost("InicializarDatos")]
    public async Task<IActionResult> InicializarDatos()
    {
        await _service.InicializarDatosAsync();
        return Ok();
    }

}
