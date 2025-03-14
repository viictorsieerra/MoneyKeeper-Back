using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models;
using Services;
namespace Controllers;

[Authorize(Roles = "Cliente")]
[ApiController]
[Route("[controller]")]
public class ReciboController : ControllerBase
{

    private readonly IReciboService _service;

    public ReciboController(IReciboService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Recibo>>> GetRecibos()
    {
        List<Recibo> recibos = await _service.GetAllAsync();
        return Ok(recibos);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Recibo>> GetRecibo(int id)
    {
        Recibo recibo = await _service.GetByIdAsync(id);
        if (recibo == null)
        {
            return NotFound();
        }
        return Ok(recibo);
    }

    [HttpPost]
    public async Task<ActionResult<Recibo>> CreateRecibo(Recibo recibo)
    {
        try
        {
            await _service.AddAsync(recibo);
            return CreatedAtAction(nameof(GetRecibo), new { id = recibo._idRecibo }, recibo);
        }
        catch (Exception ex)
        {
            // Devolver el error como JSON
            return StatusCode(500, new { message = "Hubo un error al procesar la solicitud", details = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecibo(int id, Recibo updatedRecibo)
    {
        var existingRecibo = await _service.GetByIdAsync(id);
        if (existingRecibo == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedRecibo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecibo(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

        [HttpPost("InicializarDatos")]
    public async Task<IActionResult> InicializarDatos()
    {
        await _service.InicializarDatosAsync();
        return Ok();
    }


     [HttpGet("recibos")]
    public async Task<ActionResult<List<ReciboDTO>>> GetMisRecibos()
    {
        var recibos = await _service.GetByUser(User);

        if (recibos == null || recibos.Count == 0)
        {
            return NotFound("No se encontraron recibos para este usuario.");
        }

        return Ok(recibos);
    }

}
