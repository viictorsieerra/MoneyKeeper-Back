using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

[ApiController]
[Route("[controller]")]
public class TransaccionController : ControllerBase
{

    private readonly ITransaccionService _service;

    public TransaccionController(ITransaccionService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Transaccion>>> Gettransaccions()
    {
        List<Transaccion> transaccions = await _service.GetAllAsync();
        return Ok(transaccions);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Transaccion>> GetTransaccion(int id)
    {
        Transaccion transaccion = await _service.GetByIdAsync(id);
        if (transaccion == null)
        {
            return NotFound();
        }
        return Ok(transaccion);
    }

    [HttpPost]
    public async Task<ActionResult<Transaccion>> CreateTransaccion(Transaccion transaccion)
    {
        await _service.AddAsync(transaccion);
        return CreatedAtAction(nameof(GetTransaccion), new { id = transaccion._idTransaccion }, transaccion);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTransaccion(int idTransaccion, Transaccion updatedTransaccion)
    {
        var existingTransaccion = await _service.GetByIdAsync(idTransaccion);
        if (existingTransaccion == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedTransaccion);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTransaccion(int idTransaccion)
    {
        await _service.DeleteAsync(idTransaccion);
        return NoContent();
    }


    [HttpPost("InicializarDatos")]
    public async Task<IActionResult> InicializarDatos()
    {
        await _service.InicializarDatosAsync();
        return Ok();
    }
}
