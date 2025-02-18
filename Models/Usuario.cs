namespace Models;

public class Usuario
{
    public int _idUsuario { get; set; }

    public string? _nombre { get; set; }
    public string? _apellido { get; set; }
    public string? _correo { get; set;}
    public string? _contrasena { get; set;}
    public string? _dni{get; set;}
    public DateTime _fecNacimiento{ get; set;} = DateTime.Now;

    public Usuario ()
    {}

    public Usuario(string nombre, string apellido, string correo, string contrasena, string dni, DateTime fecNacimiento){
        _nombre = nombre;
        _apellido = apellido;
        _correo = correo;
        _fecNacimiento = fecNacimiento;
        _contrasena = contrasena;
        _dni = dni;
    }
}