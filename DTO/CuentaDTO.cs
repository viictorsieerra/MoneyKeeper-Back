namespace DTO;

public class CuentaDTO
{
    public int _idCuenta { get; set; }
    public int _idUsuario { get; set; }
    public decimal _dineroCuenta { get; set; }
    public bool _activa { get; set; }
    public DateTime _fechaCreacion { get; set; } = DateTime.Now;
    public string _nombreCuenta { get; set; }

    public CuentaDTO() { }

     public CuentaDTO(int idCuenta, int idUsuario, decimal dineroCuenta, bool activa, DateTime fechaCreacion, string nombreCuenta)
    {
        _idCuenta = idCuenta;
        _idUsuario = idUsuario;
        _dineroCuenta = dineroCuenta;
        _activa = activa;
        _fechaCreacion = fechaCreacion;
        _nombreCuenta = nombreCuenta;
    }
}