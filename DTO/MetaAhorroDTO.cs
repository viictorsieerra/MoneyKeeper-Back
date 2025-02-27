namespace DTO;

public class MetaAhorroDTO
{
       public string _nombreMeta { get; set; }
    public string _descripcionMeta {get;set;}
    public decimal _dineroObjetivo {get;set;}
    public decimal _dineroActual {get;set;}
    public bool _activoMeta {get; set;} = false;
    public DateTime _fechaCreacionMeta {get; set;} = DateTime.Now;
    public DateTime _fechaObjetivoMeta {get; set;} = DateTime.Now;

    public MetaAhorroDTO() { }

   public MetaAhorroDTO(string nombreMeta, string descripcionMeta, decimal dineroObjetivo, decimal dineroActual, bool activoMeta, DateTime fechaCreacionMeta, DateTime fechaObjetivoMeta)
    {
        _nombreMeta = nombreMeta;
        _descripcionMeta = descripcionMeta;
        _dineroObjetivo = dineroObjetivo;
        _dineroActual = dineroActual;
        _activoMeta = activoMeta;
        _fechaCreacionMeta = fechaCreacionMeta;
        _fechaObjetivoMeta = fechaObjetivoMeta;

    }
}