using System.Data;

namespace Models;

public class Transaccion
{
    public int _idTransaccion {get;set;}
    public int _idUsuario { get; set; }
    public int _idCategoria { get; set; }
    public int _idCuenta { get; set; }
    public decimal _cantidad {get; set;}
    public char _tipoMovimiento {get; set;}
    public string _descripcionTransaccion {get; set;}
    public DateTime _fecTransaccion { get; set; } = DateTime.Now;

    public Transaccion(){}

    public Transaccion (int idTransaccion, int idUsuario, int idCategoria,char tipoMovimiento, decimal cantidad, string descripcionTransaccion, DateTime fecTransaccion, int idCuenta)
    {
        _idTransaccion = idTransaccion;
        _idUsuario = idUsuario;
        _idCategoria = idCategoria;
        _tipoMovimiento = tipoMovimiento;
        _cantidad = cantidad;
        _descripcionTransaccion = descripcionTransaccion;
        _fecTransaccion = fecTransaccion;
        _idCuenta = idCuenta;
    }








}