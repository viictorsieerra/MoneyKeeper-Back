using System.Security.Cryptography.X509Certificates;

namespace Models;

public class Recibo
{
    public string _nombreRecibo { get; set; }
    public int _idRecibo { get; set; }
    public int _idUsuario { get; set; }
    public int _idCuenta { get; set; }
    public decimal _dineroRecibo { get; set; }
    public bool _activa { get; set; }
    public DateTime _fecRecibo { get; set; } = DateTime.Now;

    public Recibo() { }
    public Recibo(string nombreRecibo, int idRecibo,int idUsuario, int idCuenta, decimal dineroRecibido, bool activa, DateTime fecRecibo)
    {
        _idRecibo = idRecibo;
        _nombreRecibo = nombreRecibo;
        _idUsuario = idUsuario;
        _idCuenta = idCuenta;
        _dineroRecibo = dineroRecibido;
        _activa = activa;
        _fecRecibo = fecRecibo;

    }



}