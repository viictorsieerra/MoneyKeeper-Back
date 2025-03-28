namespace DTO;

public class PresupuestoDTO
{
    public int _idPresupuesto { get; set; }
    public int _idUsuario { get; set; }
    public string _nombreCategoria { get; set; }
    public string _nombrePresupuesto { get; set; }
    public decimal _limite { get; set; }
    public decimal _dineroActual { get; set; }
    public DateTime _fecCreacion { get; set; }
    public bool _activo { get; set; }

    public PresupuestoDTO() { }

    public PresupuestoDTO(int idPresupuesto, int idUsuario, string nombreCategoria, string nombrePresupuesto, decimal limite, decimal dineroActual, DateTime fecCreacion, bool activo)
    {
        _idPresupuesto = idPresupuesto;
        _idUsuario = idUsuario;
        _nombreCategoria = nombreCategoria;
        _nombrePresupuesto = nombrePresupuesto;
        _limite = limite;
        _dineroActual = dineroActual;
        _fecCreacion = fecCreacion;
        _activo = activo;
    }
}
