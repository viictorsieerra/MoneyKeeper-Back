using Models;
using Microsoft.Data.SqlClient;

namespace Repositories;

class CategoriaRepository : ICategoriaRepository
{
    private readonly string? _connectionString;

    public CategoriaRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        List<Categoria> categorias = new List<Categoria>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idCategoria, Nombre, Descripcion FROM Cuentas";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Categoria categoria = new Categoria
                        {
                            _idCategoria = reader.GetInt32(0),
                           _nombre = reader.GetString(30),
                            _descripcion = reader.GetString(60)
                        };

                        categorias.Add(categoria);
                    }
                }
            }
        }
        return categorias;
    }

    public async Task<Categoria> GetByIdAsync(int idCategoria)
    {
        Categoria categoria = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idCategoria, Nombre, Descripcion FROM Categorias WHERE idCategoria = @idCategoria";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCategoria", idCategoria);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        categoria = new Categoria
                        {
                             _idCategoria = reader.GetInt32(0),
                           _nombre = reader.GetString(30),
                            _descripcion = reader.GetString(60)
                        };
                    }
                }
            }
        }


        return categoria;
    }

    public async Task AddAsync(Categoria categoria)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Categorias (Nombre, Descripcion) VALUES (@Nombre, @Descripcion)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", categoria._nombre);
                command.Parameters.AddWithValue("@Descripcion", categoria._descripcion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Categorias SET Nombre = @Nombre, Descripcion = @Descripcion WHERE idCategoria = @idCategoria";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", categoria._nombre);
                command.Parameters.AddWithValue("@Descripcion", categoria._descripcion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idCategoria)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Categorias WHERE idCategoria = @idCategoria";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCategoria", idCategoria);

                await command.ExecuteNonQueryAsync();
            }
        }
    }


}