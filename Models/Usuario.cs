namespace Models;

class Usuario
{
    public int _idUsuario { get; set; }

    public string? _nombre { get; set; }
    public string? _apellido { get; set; }
    public string? _correo { get; set;}
    public string? _contrasena { get; set;}
    public string? _dni{get; set;}

    public Usuario ()
    {}

    public Usuario(string nombre, string apellido, string correo, string contrasena, string dni){
        _nombre = nombre;
        _apellido = apellido;
        _correo = correo;
        _contrasena = contrasena;
        _dni = dni;
    }
}