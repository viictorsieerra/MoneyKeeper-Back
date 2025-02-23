namespace DTO;

    public class LoginDTO
    {
        public string _correo { get; set;}
        public string _contrasena { get; set;}

        public LoginDTO(string correo, string contrasena)
        {
            _correo = correo;
            _contrasena = contrasena;
        }
        public LoginDTO() { }
    }

