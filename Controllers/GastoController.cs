using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[Authorize(Roles = "Cliente")]
[ApiController]
[Route("api/[controller]")]
public class GastoController : ControllerBase
{
    private readonly IGastoService _service;

    public GastoController(IGastoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Gasto>>> GetGastos()
    {
        List<Gasto> gastos = await _service.GetAllAsync();
        return Ok(gastos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Gasto>> GetGasto(int id)
    {
        Gasto gasto = await _service.GetByIdAsync(id);
        if (gasto == null)
        {
            return NotFound();
        }
        return Ok(gasto);
    }

    [HttpPost]
    public async Task<ActionResult<Gasto>> CreateGasto(Gasto gasto)
    {
        await _service.AddAsync(gasto);
        return CreatedAtAction(nameof(GetGasto), new { id = gasto._idGasto }, gasto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGasto(int id, Gasto updatedGasto)
    {
        var existingGasto = await _service.GetByIdAsync(id);
        if (existingGasto == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedGasto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteGasto(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("byPresupuesto/{idPresupuesto}")]
    public async Task<ActionResult<List<Gasto>>> GetGastosByPresupuesto(int idPresupuesto)
    {
        var gastos = await _service.GetByPresupuestoAsync(idPresupuesto);

        if (gastos == null || gastos.Count == 0)
        {
            return NotFound("No se encontraron gastos para este presupuesto.");
        }

        return Ok(gastos);
    }
}
