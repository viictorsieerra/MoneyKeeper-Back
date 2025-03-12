using Models;

namespace DTO;

public class ReciboDTO
{
    public decimal _dineroRecibo { get; set; }
    public bool _activa { get; set; }
    public DateTime _fecRecibo { get; set; } = DateTime.Now;
    public string _nombreRecibo {get; set;}
    public int _idRecibo {get; set;}

    public ReciboDTO() { }

    public ReciboDTO(decimal dineroRecibido, bool activa, DateTime fecRecibo, string nombreRecibo, int idRecibo)
    {
        _dineroRecibo = dineroRecibido;
        _activa = activa;
        _fecRecibo = fecRecibo;
        _nombreRecibo = nombreRecibo;
        _idRecibo = idRecibo;
    }
}