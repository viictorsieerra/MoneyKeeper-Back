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

            string query = "SELECT idTransaccion, idUsuario, idCategoria, Cantidad, Descripcion, FecTransaccion, TipoMovimiento FROM Transaccion";

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
                            _fecTransaccion = reader.GetDateTime(5),
                            _tipoMovimiento = reader.GetString(6).Trim()[0],
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

            string query = "SELECT idTransaccion, idUsuario, idCategoria, Cantidad, Descripcion, FecTransaccion, TipoMovimiento FROM Transaccion WHERE idTransaccion = @idTransaccion";

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
                            _fecTransaccion = reader.GetDateTime(5),
                            _tipoMovimiento = reader.GetString(6).Trim()[0]
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

            string query = "INSERT INTO Transaccion (idUsuario, idCategoria, Cantidad, Descripcion, FecTransaccion, TipoMovimiento) VALUES (@IidUsuario, @idCategoria, @Cantidad, @Descripcion, @FecTransaccion, @TipoMovimiento)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IidUsuario", transaccion._idUsuario);
                command.Parameters.AddWithValue("@idCategoria", transaccion._idCategoria);
                command.Parameters.AddWithValue("@Cantidad", transaccion._cantidad);
                command.Parameters.AddWithValue("@Descripcion", transaccion._descripcionTransaccion);
                command.Parameters.AddWithValue("@FecTransaccion", transaccion._fecTransaccion);
                command.Parameters.AddWithValue("@TipoMovimiento",transaccion._tipoMovimiento);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Transaccion transaccion)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Transaccion SET idUsuario = @IidUsuario, idCategoria = @idCategoria, Cantidad = @Cantidad, Descripcion = @Descripcion, FecTransaccion = @FecTransaccion, TipoMovimiento = @TipoMovimiento  WHERE idTransaccion = @idTransaccion";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IidUsuario", transaccion._idUsuario);
                command.Parameters.AddWithValue("@idCategoria", transaccion._idCategoria);
                command.Parameters.AddWithValue("@Cantidad", transaccion._cantidad);
                command.Parameters.AddWithValue("@Descripcion", transaccion._descripcionTransaccion);
                command.Parameters.AddWithValue("@FecTransaccion", transaccion._fecTransaccion);
                command.Parameters.AddWithValue("@TipoMovimiento", transaccion._tipoMovimiento);
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
