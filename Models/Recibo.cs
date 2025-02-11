using System.Security.Cryptography.X509Certificates;

namespace Models;

class Recibo
{
    public int _idRecibo { get; set; }
    public int _idUsuario { get; set; }
    public int _idCuenta { get; set; }
    public decimal _dineroRecibo { get; set; }
    public bool _activa { get; set; }
    public DateTime _fec_Creacion { get; set; } = DateTime.Now;

    public Recibo() { }
    public Recibo(int idUsuario, int idCuenta, decimal dineroRecibido, bool activa, DateTime fec_Creacion)
    {
        _idUsuario = idUsuario;
        _idCuenta = idCuenta;
        _dineroRecibo = dineroRecibido;
        _activa = activa;
        _fec_Creacion = fec_Creacion;

    }



}