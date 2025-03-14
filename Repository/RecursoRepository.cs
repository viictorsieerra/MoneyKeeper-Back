using System.Data;
using Microsoft.Data.SqlClient;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class RecursoRepository : IRecursoRepository
{
    private readonly string? _connectionString;

    public RecursoRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<IEnumerable<Recurso>> GetAll()
    {
        List<Recurso> recursos = new List<Recurso>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = "SELECT id, nombre FROM Recursos";  

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        recursos.Add(new Recurso
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        });
                    }
                }
            }
        }

        return recursos;
    }

    public async Task<Recurso?> GetById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = "SELECT id, nombre FROM Recursos WHERE id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Recurso
                        {
                            Id = reader.GetInt32(0),
                            Nombre = reader.GetString(1)
                        };
                    }
                }
            }
        }

        return null;
    }

    public async Task<Recurso> Add(Recurso recurso)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = "INSERT INTO Recursos (nombre) OUTPUT Inserted.id VALUES (@nombre)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombre", recurso.Nombre);

                recurso.Id = (int)await command.ExecuteScalarAsync();
            }
        }

        return recurso;
    }

    public async Task<bool> Delete(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = "DELETE FROM Recursos WHERE id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                int affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
        }
    }
}
