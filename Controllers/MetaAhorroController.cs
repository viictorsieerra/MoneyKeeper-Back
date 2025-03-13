using DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

[Authorize(Roles = "Cliente")]
[ApiController]
[Route("[controller]")]
public class MetaAhorroController : ControllerBase
{

    private readonly IMetaAhorroService _service;

    public MetaAhorroController(IMetaAhorroService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<MetaAhorro>>> GetMetas()
    {
        List<MetaAhorro> metas = await _service.GetAllAsync();
        return Ok(metas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MetaAhorro>> GetMeta(int id)
    {
        MetaAhorro meta = await _service.GetByIdAsync(id);
        if (meta == null)
        {
            return NotFound();
        }
        return Ok(meta);
    }

    [HttpPost]
    public async Task<ActionResult<MetaAhorro>> CreateMeta(MetaAhorro meta)
    {
        await _service.AddAsync(meta);
        return CreatedAtAction(nameof(GetMeta), new { id = meta._idMeta }, meta);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MetaAhorro>> UpdateMeta(int id, MetaAhorro updatedMetaAhorro)
    {
        var existingMetaAhorro = await _service.GetByIdAsync(id);
        if (existingMetaAhorro == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedMetaAhorro);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMetaAhorro(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("metas")]
    public async Task<ActionResult<List<MetaAhorroDTO>>> GetMisMetas()
    {
        var metas = await _service.GetByUser(User);

        if (metas == null || metas.Count == 0)
        {
            return NotFound("No se encontraron metas para este usuario.");
        }

        return Ok(metas);
    }


}
