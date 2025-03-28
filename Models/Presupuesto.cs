namespace Models;
public class Presupuesto
{
    public int? _idPresupuesto { get; set; }
    public int? _idUsuario { get; set; }
    public int? _idCategoria { get; set; } = 4;
    public string? _nombre { get; set; } = "Presupuesto";
    public decimal? _limite { get; set; } = 10000;
    public decimal? _dineroActual { get; set; } = 0;
    public bool? _activo { get; set; } = true;
    public DateTime? _fecCreacion { get; set; } = DateTime.Now;
    public Presupuesto() { }

    public Presupuesto(int? idPresupuesto, int? idUsuario, int? idCategoria, string? nombre, decimal? limite, decimal? dineroActual, bool? activo)
    {
        _idPresupuesto = idPresupuesto;
        _idUsuario = idUsuario;
        _idCategoria = idCategoria;
        _nombre = nombre;
        _limite = limite;
        _dineroActual = dineroActual;
        _activo = activo;
    }

}