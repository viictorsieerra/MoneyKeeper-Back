using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using DTO;
using Repositories;
using Models;

namespace Services;
    public class JwtService :IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsuarioRepository? _repository;
        public JwtService (IConfiguration configuration, IUsuarioRepository repository)
        {
            _configuration = configuration;
            _repository = repository;
        }

        public async Task<string> GenerateToken(UsuarioDTOOut usuarioDTOOut)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, usuarioDTOOut._idUsuario.ToString()),
                    new Claim(ClaimTypes.Email, usuarioDTOOut._correo),
                    new Claim(ClaimTypes.Role, "Cliente")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public async Task<string> Login(LoginDTO loginDTO)
        {
            UsuarioDTOOut user = await _repository.GetUserFromCredentials(loginDTO);
            return await GenerateToken(user);
        }

        public async Task<string> Register (Usuario userRegistered)
        {
            UsuarioDTOOut user = await _repository.RegisterUser(userRegistered);

            return await GenerateToken(user);
        }
    }