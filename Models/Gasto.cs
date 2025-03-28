namespace Models;
/*
	IdGasto INT IDENTITY(1,1) PRIMARY KEY,
    IdPresupuesto INT NOT NULL,
    Nombre NVARCHAR(75) NOT NULL DEFAULT 'Gasto',
	Descripcion NVARCHAR(200) NOT NULL DEFAULT 'Gasto',
    Cantidad DECIMAL(10,2) NOT NULL DEFAULT 0,
*/
public class Gasto
{
    public int? _idPresupuesto { get; set; }
    public int? _idGasto { get; set; }
    public string? _nombre { get; set; } = "Gasto";
    public string? _descripcion { get; set; } = "Gasto";
    public decimal? _cantidad { get; set; } = 0;
    public DateTime? _fecCreacion {get; set;} = DateTime.Now;
    public Gasto() { }

    public Gasto(int? idPresupuesto, int? idGasto, string? nombre, string? descripcion, decimal? cantidad)
    {
        _idPresupuesto = idPresupuesto;
        _idGasto = idGasto;
        _nombre = nombre;
        _descripcion = descripcion;
        _cantidad = cantidad;
    }

}