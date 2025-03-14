namespace Models;

public class MetaAhorro
{
    public int _idMeta {get;set;}
    public int _idUsuario { get; set; }
    public string? _nombreMeta { get; set; }
    public string? _descripcionMeta {get;set;}
    public decimal _dineroObjetivo {get;set;}
    public decimal _dineroActual {get;set;}
    public bool _activoMeta {get; set;} = false;
    public DateTime _fechaCreacionMeta {get; set;} = DateTime.Now;
    public DateTime _fechaObjetivoMeta {get; set;} = DateTime.Now;

    public MetaAhorro()
    {}

    public MetaAhorro(int idMeta, int idUsuario, string nombreMeta, string descripcionMeta, decimal dineroObjetivo, decimal dineroActual, bool activoMeta, DateTime fechaCreacionMeta, DateTime fechaObjetivoMeta)
    {
        _idMeta = idMeta;
        _idUsuario = idUsuario;
        _nombreMeta = nombreMeta;
        _descripcionMeta = descripcionMeta;
        _dineroObjetivo = dineroObjetivo;
        _dineroActual = dineroActual;
        _activoMeta = activoMeta;
        _fechaCreacionMeta = fechaCreacionMeta;
        _fechaObjetivoMeta = fechaObjetivoMeta;

    }





}