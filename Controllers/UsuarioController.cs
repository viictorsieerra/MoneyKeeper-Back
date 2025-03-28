using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
namespace Controllers;

[Authorize(Roles = "Cliente")]
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioService _service;

    public UsuarioController(IUsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<Usuario>>> GetUsuarios()
    {
        List<Usuario> usuarios = await _service.GetAllAsync();
        return Ok(usuarios);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        Usuario usuario = await _service.GetByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }
    [HttpGet("Auth")]
    public async Task<ActionResult<Usuario>> GetUsuarioAuth()
    {
        Usuario usuario = await _service.GetByToken(User);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [HttpPost]
    public async Task<ActionResult<Usuario>> CreateUsuario(Usuario usuario)
    {
        await _service.AddAsync(usuario);
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario._idUsuario }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, Usuario updatedUsuario)
    {
        updatedUsuario._idUsuario = id;
        var existingUsuario = await _service.GetByIdAsync(id);
        if (existingUsuario == null)
        {
            return NotFound();
        }

        await _service.UpdateAsync(updatedUsuario);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int idUsuario)
    {
        await _service.DeleteAsync(idUsuario);
        return NoContent();
    }

    [HttpPost("InicializarDatos")]
    public async Task<IActionResult> InicializarDatos()
    {
        await _service.InicializarDatosAsync();
        return Ok();
    }

}
