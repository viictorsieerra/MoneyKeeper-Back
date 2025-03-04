using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

[Authorize (Roles = "Cliente")]
[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{

    private readonly ICategoriaService _service;

    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Categoria>>> GetCategorias()
    {
        List<Categoria> categorias = await _service.GetAllAsync();
        return Ok(categorias);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        Categoria categoria = await _service.GetByIdAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }
        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<Categoria>> CreateCategoria(Categoria categoria)
    {
        await _service.AddAsync(categoria);
        return CreatedAtAction(nameof(GetCategoria), new { id = categoria._idCategoria }, categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategoria(int idCategoria, Categoria updatedCategoria)
    {
        var existingCategoria = await _service.GetByIdAsync(idCategoria);
        if (existingCategoria == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedCategoria);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int idCategoria)
    {
        await _service.DeleteAsync(idCategoria);
        return NoContent();
    }

    [HttpPost("InicializarDatos")]
    public async Task<IActionResult> InicializarDatos()
    {
        await _service.InicializarDatosAsync();
        return Ok();
    }

}
