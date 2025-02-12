using System.Security.Cryptography.X509Certificates;

namespace Models;

public class Cuenta
{
    public int _idCuenta { get; set; } = 0;
    public int _idUsuario { get; set; } = 0;
    public decimal _dineroCuenta { get; set; } = 0;
    public bool _activa { get; set; } = false;
    public DateTime _fechaCreacion { get; set; } = DateTime.Now;

    public Cuenta() { }

    public Cuenta(int idCuenta, int idUsuario, decimal dineroCuenta, bool activa, DateTime fechaCreacion)
    {
        _idCuenta = idCuenta;
        _idUsuario = idUsuario;
        _dineroCuenta = dineroCuenta;
        _activa = activa;
        _fechaCreacion = fechaCreacion;
    }
}