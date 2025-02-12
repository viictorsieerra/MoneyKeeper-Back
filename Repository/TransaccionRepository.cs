using Models;
using Microsoft.Data.SqlClient;

namespace Repositories;

class TransaccionRepository : ITransaccionRepository
{
    private readonly string? _connectionString;

    public TransaccionRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Transaccion>> GetAllAsync()
    {
        List<Transaccion> transacciones = new List<Transaccion>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idTransaccion, idUsuario, idCategoria, Cantidad, Descripcion, Fec_transaccion FROM Transaccion";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Transaccion transaccion = new Transaccion
                        {
                            _idTransaccion = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _idCategoria = reader.GetInt32(2),
                            _cantidad = reader.GetInt32(3),
                            _descripcionTransaccion = reader.GetString(4),
                            _fec_Transaccion = reader.GetDateTime(5)
                        };

                        transacciones.Add(transaccion);
                    }
                }
            }
        }
        return transacciones;
    }

    public async Task<Transaccion> GetByIdAsync(int idTransaccion)
    {
        Transaccion transaccion = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idTransaccion, idUsuario, idCategoria, Cantidad, Descripcion, Fec_transaccion FROM Transaccion FROM Transaccion WHERE idTransaccion = @idTransaccion";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idTransaccion", idTransaccion);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        transaccion = new Transaccion
                        { 
                            _idTransaccion = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _idCategoria = reader.GetInt32(2),
                            _cantidad = reader.GetInt32(3),
                            _descripcionTransaccion = reader.GetString(4),
                            _fec_Transaccion = reader.GetDateTime(5)
                        };
                    }
                }
            }
        }

        return transaccion;
    }

    public async Task AddAsync(Transaccion transaccion)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Transaccion (idUsuario, idCategoria, Cantidad, Descripcion, Fec_transaccion) VALUES (@IidUsuario, @idCategoria, @Cantidad, @Descripcion, @Fec_transaccion)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IidUsuario", transaccion._idUsuario);
                command.Parameters.AddWithValue("@idCategoria", transaccion._idCategoria);
                command.Parameters.AddWithValue("@Cantidad", transaccion._cantidad);
                command.Parameters.AddWithValue("@Descripcion", transaccion._descripcionTransaccion);
                command.Parameters.AddWithValue("@Fec_transaccion", transaccion._fec_Transaccion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Transaccion transaccion)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Transaccion SET idUsuario = @IidUsuario, idCategoria = @idCategoria, Cantidad = @Cantidad, Descripcion = @Descripcion, Fec_transaccion = @Fec_transaccion WHERE idTransaccion = @idTransaccion";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IidUsuario", transaccion._idUsuario);
                command.Parameters.AddWithValue("@idCategoria", transaccion._idCategoria);
                command.Parameters.AddWithValue("@Cantidad", transaccion._cantidad);
                command.Parameters.AddWithValue("@Descripcion", transaccion._descripcionTransaccion);
                command.Parameters.AddWithValue("@Fec_transaccion", transaccion._fec_Transaccion);
                command.Parameters.AddWithValue("@idTransaccion", transaccion._idTransaccion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idTransaccion)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Transaccion WHERE idTransaccion = @idTransaccion";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idTransaccion", idTransaccion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
