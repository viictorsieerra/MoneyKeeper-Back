using DTO;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

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
        await _service.AddAsync(recibo);
        return CreatedAtAction(nameof(GetRecibo), new { id = recibo._idRecibo }, recibo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecibo(int idRecibo, Recibo updatedRecibo)
    {
        var existingRecibo = await _service.GetByIdAsync(idRecibo);
        if (existingRecibo == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedRecibo);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecibo(int idRecibo)
    {
        await _service.DeleteAsync(idRecibo);
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
