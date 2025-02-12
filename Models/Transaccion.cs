using System.Data;

namespace Models;

class Transaccion
{
    public int _idTransaccion {get;set;}
    public int _idUsuario { get; set; }
    public int _idCategoria { get; set; }
    public int _cantidad {get; set;}
    public string _descripcionTransaccion {get; set;}
    public DateTime _fec_Transaccion { get; set; } = DateTime.Now;

    public Transaccion(){}

    public Transaccion (int idTransaccion, int idUsuario, int idCategoria, int cantidad, string descripcionTransaccion, DateTime fec_Transaccion)
    {
        _idTransaccion = idTransaccion;
        _idUsuario = idUsuario;
        _idCategoria = idCategoria;
        _cantidad = cantidad;
        _descripcionTransaccion = descripcionTransaccion;
        _fec_Transaccion = fec_Transaccion;
    }








}