using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[Authorize(Roles = "Cliente")]
[ApiController]
[Route("api/[controller]")]
public class PresupuestoController : ControllerBase
{
    private readonly IPresupuestoService _service;

    public PresupuestoController(IPresupuestoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Presupuesto>>> GetPresupuestos()
    {
        List<Presupuesto> presupuestos = await _service.GetAllAsync();
        return Ok(presupuestos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Presupuesto>> GetPresupuesto(int id)
    {
        Presupuesto presupuesto = await _service.GetByIdAsync(id);
        if (presupuesto == null)
        {
            return NotFound();
        }
        return Ok(presupuesto);
    }

    [HttpPost]
    public async Task<ActionResult<Presupuesto>> CreatePresupuesto(Presupuesto presupuesto)
    {
        await _service.AddAsync(presupuesto);
        return CreatedAtAction(nameof(GetPresupuesto), new { id = presupuesto._idPresupuesto }, presupuesto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePresupuesto(int id, Presupuesto updatedPresupuesto)
    {
        var existingPresupuesto = await _service.GetByIdAsync(id);
        if (existingPresupuesto == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedPresupuesto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePresupuesto(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("presupuestos")]
    public async Task<ActionResult<List<PresupuestoDTO>>> GetPresupuestosByUsuario()
    {
        var presupuestos = await _service.GetByUser(User);

        if (presupuestos == null || presupuestos.Count == 0)
        {
            return NotFound("No se encontraron presupuestos para este usuario.");
        }

        return Ok(presupuestos);
    }
}
