namespace DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MetaAhorroDTO
{
    public int idMeta { get; set; }
    public string _nombreMeta { get; set; }
    public string _descripcionMeta {get;set;}
    public decimal _dineroObjetivo {get;set;}
    public decimal _dineroActual {get;set;}
    public bool _activoMeta {get; set;} = false;
    public DateTime _fechaCreacionMeta {get; set;} = DateTime.Now;
    public DateTime _fechaObjetivoMeta {get; set;} = DateTime.Now;

    public MetaAhorroDTO() { }

   public MetaAhorroDTO(int idMeta, string nombreMeta, string descripcionMeta, decimal dineroObjetivo, decimal dineroActual, bool activoMeta, DateTime fechaCreacionMeta, DateTime fechaObjetivoMeta)
    {
        idMeta = idMeta;
        _nombreMeta = nombreMeta;
        _descripcionMeta = descripcionMeta;
        _dineroObjetivo = dineroObjetivo;
        _dineroActual = dineroActual;
        _activoMeta = activoMeta;
        _fechaCreacionMeta = fechaCreacionMeta;
        _fechaObjetivoMeta = fechaObjetivoMeta;

    }
}