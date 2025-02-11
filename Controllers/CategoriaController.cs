using Microsoft.AspNetCore.Mvc;
using Models;
namespace Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<CategoriaController> _logger;

    public CategoriaController(ILogger<CategoriaController> logger)
    {
        _logger = logger;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    /*
    public IEnumerable<Categoria> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Categoria
        {

        })
        .ToArray();
    }*/
}
