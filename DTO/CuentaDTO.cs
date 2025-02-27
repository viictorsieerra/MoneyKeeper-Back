namespace DTO;

public class CuentaDTO
{
    public decimal _dineroCuenta { get; set; }
    public bool _activa { get; set; }
    public DateTime _fechaCreacion { get; set; } = DateTime.Now;
    public string _nombreCuenta { get; set; }

    public CuentaDTO() { }

     public CuentaDTO( decimal dineroCuenta, bool activa, DateTime fechaCreacion, string nombreCuenta)
    {
       
        _dineroCuenta = dineroCuenta;
        _activa = activa;
        _fechaCreacion = fechaCreacion;
        _nombreCuenta = nombreCuenta;
    }
}