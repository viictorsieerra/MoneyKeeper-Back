using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

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
    public async Task<ActionResult<List<MetaAhorro>>> GetMetaAhorro()
    {
        List<MetaAhorro> metas = await _service.GetAllAsync();
        return Ok(metas);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<MetaAhorro>> GetMetaAhorro(int id)
    {
        MetaAhorro meta = await _service.GetByIdAsync(id);
        if (meta == null)
        {
            return NotFound();
        }
        return Ok(meta);
    }

    [HttpPost]
    public async Task<ActionResult<MetaAhorro>> CreateMetaAhorro(MetaAhorro meta)
    {
        await _service.AddAsync(meta);
        return CreatedAtAction(nameof(GetMetaAhorro), new { id = MetaAhorro._idMeta }, meta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMetaAhorro(int idMeta, MetaAhorro updatedMetaAhorro)
    {
        var existingMetaAhorro = await _service.GetByIdAsync(idMeta);
        if (existingMetaAhorro == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedMetaAhorro);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMetaAhorro(int idMeta)
    {
        await _service.DeleteAsync(idMeta);
        return NoContent();
    }

}
