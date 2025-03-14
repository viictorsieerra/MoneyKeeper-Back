namespace DTO;

public class TransaccionDTO
{
    public int _idTransaccion { get; set; }
    public string _nombreCategoria { get; set; }
    public decimal _cantidad { get; set; }
    public char _tipoMovimiento { get; set; }
    public string _descripcionTransaccion { get; set; }
    public DateTime _fecTransaccion { get; set; } = DateTime.Now;

    public TransaccionDTO() { }

    public TransaccionDTO(int idTransaccion, string nombreCategoria, char tipoMovimiento, decimal cantidad, string descripcionTransaccion, DateTime fecTransaccion)
    {
        _idTransaccion = idTransaccion;
        _nombreCategoria = nombreCategoria;
        _tipoMovimiento = tipoMovimiento;
        _cantidad = cantidad;
        _descripcionTransaccion = descripcionTransaccion;
        _fecTransaccion = fecTransaccion;
    }
}