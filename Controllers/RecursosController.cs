

using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models;
using Services;
namespace Controllers;
[ApiController]
[Route("api/[controller]")]
public class RecursosController : ControllerBase
{
    private readonly RecursoService _service;

    public RecursosController(RecursoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recurso>>> GetRecursos()
    {
        return Ok(await _service.GetAll());
    }

    [HttpPost]
    public async Task<ActionResult<Recurso>> PostRecurso(RecursoDTO recursoDTO)
    {
        var recurso = await _service.Add(recursoDTO);
        return CreatedAtAction(nameof(PostRecurso), new { id = recurso.Id }, recurso);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecurso(int id)
    {
        var deleted = await _service.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}