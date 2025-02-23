using System.Security.Claims;
using DTO;
using Models;

namespace Services;
    public interface IJwtService
    {
        Task <string> GenerateToken(UsuarioDTOOut usuarioDTOOut);
        Task<string> Login(LoginDTO usuarioDTO);

        Task<string> Register(Usuario usuario);
    }
