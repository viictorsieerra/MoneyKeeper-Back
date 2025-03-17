using Microsoft.AspNetCore.Mvc;
using Models;
using DTO;
using Services;

namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class JWTController : ControllerBase
{

    private readonly IJwtService _service;

    public JWTController(IJwtService service)
    {
        _service = service;
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login(LoginDTO cuenta)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = await _service.Login(cuenta);
            return Ok(token);
        }
        catch (KeyNotFoundException ex)
        {
            return Unauthorized(ex);
        }
        catch (Exception ex) {
            return BadRequest("Error al generar el token");
        }
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(Usuario cuenta)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            string token = await _service.Register(cuenta);
            return Ok(token);
        }
        catch (KeyNotFoundException ex)
        {
            return Unauthorized(ex);
        }
        catch (Exception ex)
        {
            return BadRequest("Error al generar el token");
        }
    }

}
