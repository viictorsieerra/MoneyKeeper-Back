namespace DTO;
    public class UsuarioDTOOut
    {
        public int _idUsuario {  get; set; }
        public string _correo { get; set; }

        public UsuarioDTOOut(int idUsuario, string correo) {
            _idUsuario = idUsuario;
            _correo = correo;
        }
        public UsuarioDTOOut() { }
    }

