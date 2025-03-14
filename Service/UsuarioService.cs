using Models;
using Microsoft.Data.SqlClient;
using Repositories;
using System.Security.Claims;
namespace Services;

class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository? _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        List<Usuario> usuarios = await _repository.GetAllAsync();
        return usuarios;
    }

    public async Task<Usuario> GetByIdAsync(int idUsuario)
    {
        Usuario? usuario = await _repository.GetByIdAsync(idUsuario);
        if (usuario == null)
        {
            throw new Exception("No se han encontrado datos");
        }
        return usuario;
    }

    public async Task<Usuario> GetByToken(ClaimsPrincipal user)
    {

        var idClaim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (idClaim == null)
        {
            return null;
        }

        int idUsuario;
        if (!int.TryParse(idClaim.Value, out idUsuario))
        {
            throw new Exception("Fallo al cambiar la ID");
        }

        Usuario usuario = await _repository.GetByIdAsync(idUsuario);
        return usuario;
    }


    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        await _repository.AddAsync(usuario);
        return usuario;
    }


    public async Task<Usuario> UpdateAsync(Usuario updatedUsuario)
    {
        var existingUsuario = await _repository.GetByIdAsync(updatedUsuario._idUsuario);

        if (existingUsuario == null)
        {
            throw new Exception("NO SE HAN ENCONTRADO DATOS");
        }

        // Actualizar cuenta
        existingUsuario._idUsuario = updatedUsuario._idUsuario;
        existingUsuario._nombre = updatedUsuario._nombre;
        existingUsuario._apellido = updatedUsuario._apellido;
        existingUsuario._correo = updatedUsuario._correo;
        existingUsuario._contrasena = updatedUsuario._contrasena;
        existingUsuario._dni = updatedUsuario._dni;



        await _repository.UpdateAsync(existingUsuario);

        return existingUsuario;
    }


    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);

    }

    public async Task InicializarDatosAsync()
    {
        await _repository.InicializarDatosAsync();
    }


}