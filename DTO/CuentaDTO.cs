namespace DTO;

public class CuentaDTO
{
    public int _idCuenta { get; set; } = 0;
    public decimal _dineroCuenta { get; set; }
    public bool _activa { get; set; }
    public DateTime _fechaCreacion { get; set; } = DateTime.Now;
    public string _nombreCuenta { get; set; }

    public CuentaDTO() { }

     public CuentaDTO( int idCuenta, decimal dineroCuenta, bool activa, DateTime fechaCreacion, string nombreCuenta)
    {
        _idCuenta = idCuenta;
        _dineroCuenta = dineroCuenta;
        _activa = activa;
        _fechaCreacion = fechaCreacion;
        _nombreCuenta = nombreCuenta;
    }
}