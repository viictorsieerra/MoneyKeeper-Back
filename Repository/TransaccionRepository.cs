using Models;
using Microsoft.Data.SqlClient;
using DTO;

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

            string query = "SELECT idTransaccion, idUsuario, idCategoria, idCuenta, Cantidad, Descripcion, FecTransaccion, TipoMovimiento FROM Transaccion";

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
                            _idCuenta = reader.GetInt32(3),
                            _cantidad = reader.GetDecimal(4),
                            _descripcionTransaccion = reader.GetString(5),
                            _fecTransaccion = reader.GetDateTime(6),
                            _tipoMovimiento = reader.GetString(7).Trim()[0],
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

            string query = "SELECT idTransaccion, idUsuario, idCategoria, idCuenta, Cantidad, Descripcion, FecTransaccion, TipoMovimiento FROM Transaccion WHERE idTransaccion = @idTransaccion";

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
                            _idCuenta = reader.GetInt32(3),
                            _cantidad = reader.GetDecimal(4),
                            _descripcionTransaccion = reader.GetString(5),
                            _fecTransaccion = reader.GetDateTime(6),
                            _tipoMovimiento = reader.GetString(7).Trim()[0],
                        };
                    }
                }
            }
        }

        return transaccion;
    }

    public async Task<List<TransaccionDTO>> GetByUser(string idUsuario)
    {
        List<TransaccionDTO> transacciones = new List<TransaccionDTO>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT ca.Nombre, Cantidad, tr.Descripcion, FecTransaccion, TipoMovimiento, idTransaccion FROM Transaccion tr\n"
                + "INNER JOIN Usuario us ON tr.idUsuario = us.idUsuario\n" +
                "INNER JOIN Categoria ca ON tr.idCategoria = ca.idCategoria WHERE us.idUsuario = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        TransaccionDTO transaccion = new TransaccionDTO
                        {
                            _nombreCategoria = reader.GetString(0),
                            _cantidad = reader.GetDecimal(1),
                            _descripcionTransaccion = reader.GetString(2),
                            _fecTransaccion = reader.GetDateTime(3),
                            _tipoMovimiento = reader.GetString(4).Trim()[0],
                            _idTransaccion = reader.GetInt32(5)
                        };

                        transacciones.Add(transaccion);
                    }
                }
            }
        }
        return transacciones;
    }

    public async Task<List<TransaccionDTO>> GetByUserFilter(string idUsuario, string fechaInicio, string fechaFin)
    {
        List<TransaccionDTO> transacciones = new List<TransaccionDTO>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT ca.Nombre, Cantidad, tr.Descripcion, FecTransaccion, TipoMovimiento, idTransaccion FROM Transaccion tr\n"
                + "INNER JOIN Usuario us ON tr.idUsuario = us.idUsuario\n" +
                "INNER JOIN Categoria ca ON tr.idCategoria = ca.idCategoria WHERE us.idUsuario = @id\n"+
                "AND FecTransaccion BETWEEN  @fechaInicio AND @fechaFin";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idUsuario);
                command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                command.Parameters.AddWithValue("@fechaFin", fechaFin);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        TransaccionDTO transaccion = new TransaccionDTO
                        {
                            _nombreCategoria = reader.GetString(0),
                            _cantidad = reader.GetDecimal(1),
                            _descripcionTransaccion = reader.GetString(2),
                            _fecTransaccion = reader.GetDateTime(3),
                            _tipoMovimiento = reader.GetString(4).Trim()[0],
                            _idTransaccion = reader.GetInt32(5),
                        };

                        transacciones.Add(transaccion);
                    }
                }
            }
        }
        return transacciones;
    }


    public async Task AddAsync(Transaccion transaccion)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Transaccion (idUsuario, idCategoria, idCuenta, Cantidad, Descripcion, FecTransaccion, TipoMovimiento) VALUES (@IidUsuario, @idCategoria, @idCuenta, @Cantidad, @Descripcion, @FecTransaccion, @TipoMovimiento)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IidUsuario", transaccion._idUsuario);
                command.Parameters.AddWithValue("@idCategoria", transaccion._idCategoria);
                command.Parameters.AddWithValue("idCuenta", transaccion._idCuenta);
                command.Parameters.AddWithValue("@Cantidad", transaccion._cantidad);
                command.Parameters.AddWithValue("@Descripcion", transaccion._descripcionTransaccion);
                command.Parameters.AddWithValue("@FecTransaccion", transaccion._fecTransaccion);
                command.Parameters.AddWithValue("@TipoMovimiento", transaccion._tipoMovimiento);


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
                // command.Parameters.AddWithValue("idCuenta", transaccion._idCuenta);
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

    public async Task InicializarDatosAsync()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            string query = @"INSERT INTO Transaccion (IdUsuario, IdCategoria, Cantidad, TipoMovimiento, Descripcion) VALUES
                            (3, 1, 50.75, 'G', 'Compra en supermercado'), (3, 2, 20.00, 'G', 'Billete de autobús'), (4, 3, 100.00, 'I', 'Venta de un objeto personal');";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
