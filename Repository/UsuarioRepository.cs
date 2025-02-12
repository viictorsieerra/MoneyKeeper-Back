using Models;
using Microsoft.Data.SqlClient;

namespace Repositories;

class UsuarioRepository : IUsuarioRepository
{
    private readonly string? _connectionString;

    public UsuarioRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        List<Usuario> usuarios = new List<Usuario>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idUsuario, Nombre, Apellido, Correo, dni FROM Usuario";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Usuario usuario = new Usuario
                        {
                            _idUsuario = reader.GetInt32(0),
                            _nombre = reader.GetString(1),
                            _apellido = reader.GetString(2),
                            _correo = reader.GetString(3),
                            _dni = reader.GetString(4)
                        };

                        usuarios.Add(usuario);
                    }
                }
            }
        }
        return usuarios;
    }

    public async Task<Usuario> GetByIdAsync(int idUsuario)
    {
        Usuario usuario = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idUsuario, Nombre, Apellido, Correo, dni FROM Usuario WHERE idUsuario = @idUsuario";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        usuario = new Usuario
                        {
                            _idUsuario = reader.GetInt32(0),
                            _nombre = reader.GetString(1),
                            _apellido = reader.GetString(2),
                            _correo = reader.GetString(3),
                            _dni = reader.GetString(4)
                        };
                    }
                }
            }
        }

        return usuario;
    }

    public async Task AddAsync(Usuario usuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Usuario (Nombre, Apellido, Correo, dni) VALUES (@Nombre, @Apellido, @Correo, @dni)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", usuario._nombre);
                command.Parameters.AddWithValue("@Apellido", usuario._apellido);
                command.Parameters.AddWithValue("@Correo", usuario._correo);
                command.Parameters.AddWithValue("@dni", usuario._dni);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, Correo = @Correo, dni = @DNI WHERE idUsuario = @idUsuario";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", usuario._nombre);
                command.Parameters.AddWithValue("@Apellido", usuario._apellido);
                command.Parameters.AddWithValue("@Correo", usuario._correo);
                command.Parameters.AddWithValue("@dni", usuario._dni);
                command.Parameters.AddWithValue("@idUsuario", usuario._idUsuario);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idUsuario)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Usuario WHERE idUsuario = @idUsuario";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
