using Models;
using Microsoft.Data.SqlClient;
using DTO;

namespace Repositories;

class ReciboRepository : IReciboRepository
{
    private readonly string? _connectionString;

    public ReciboRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Recibo>> GetAllAsync()
    {
        List<Recibo> recibos = new List<Recibo>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT nombreRecibo, idRecibo, idUsuario, idCuenta, Dinero, Activo, FecRecibo FROM Recibos";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Recibo recibo = new Recibo
                        {
                            _nombreRecibo = reader.GetString(0),
                            _idRecibo = reader.GetInt32(1),
                            _idUsuario = reader.GetInt32(2),
                            _idCuenta = reader.GetInt32(3),
                            _dineroRecibo = reader.GetDecimal(4),
                            _activa = reader.GetBoolean(5),
                            _fecRecibo = reader.GetDateTime(6)
                        };

                        recibos.Add(recibo);
                    }
                }
            }
        }
        return recibos;
    }

    public async Task<Recibo> GetByIdAsync(int idRecibo)
    {
        Recibo recibo = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT nombreRecibo, idRecibo, idUsuario, idCuenta, Dinero, Activo, FecRecibo FROM Recibos WHERE idRecibo = @idRecibo";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idRecibo", idRecibo);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        recibo = new Recibo
                        {
                            _nombreRecibo = reader.GetString(0),
                            _idRecibo = reader.GetInt32(1),
                            _idUsuario = reader.GetInt32(2),
                            _idCuenta = reader.GetInt32(3),
                            _dineroRecibo = reader.GetDecimal(4),
                            _activa = reader.GetBoolean(5),
                            _fecRecibo = reader.GetDateTime(6)
                        };
                    }
                }
            }
        }
        return recibo;
    }

    public async Task AddAsync(Recibo recibo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Recibos (nombreRecibo, idUsuario, idCuenta, Dinero, Activo, FecRecibo) VALUES (@nombreRecibo, @idUsuario, @idCuenta, @Dinero, @Activo, @FecRecibo)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombreRecibo", recibo._nombreRecibo);
                command.Parameters.AddWithValue("@ID_Usuario", recibo._idUsuario);
                command.Parameters.AddWithValue("@ID_Cuenta", recibo._idCuenta);
                command.Parameters.AddWithValue("@Dinero", recibo._dineroRecibo);
                command.Parameters.AddWithValue("@Activo", recibo._activa);
                command.Parameters.AddWithValue("@FecRecibo", recibo._fecRecibo);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Recibo recibo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Recibos SET nombreRecibo = @nombreRecibo, idUsuario = @idUsuario, idCuenta = @idCuenta, Dinero = @Dinero, Activo = @Activo, FecRecibo = @FecRecibo WHERE idRecibo = @idRecibo";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@nombreRecibo", recibo._nombreRecibo);
                command.Parameters.AddWithValue("@ID_Usuario", recibo._idUsuario);
                command.Parameters.AddWithValue("@ID_Cuenta", recibo._idCuenta);
                command.Parameters.AddWithValue("@Dinero", recibo._dineroRecibo);
                command.Parameters.AddWithValue("@Activo", recibo._activa);
                command.Parameters.AddWithValue("@FecRecibo", recibo._fecRecibo);
                command.Parameters.AddWithValue("@ID_Recibo", recibo._idRecibo);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idRecibo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Recibos WHERE idRecibo = @idRecibo";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idRecibo", idRecibo);
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task InicializarDatosAsync()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"INSERT INTO Recibos (IdCuenta, IdUsuario, Dinero) VALUES (2, 3, 1200.00), (3, 4, 800.00);";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<List<ReciboDTO>> GetByUser(string idUsuario)
    {
        List<ReciboDTO> recibos = new List<ReciboDTO>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT Dinero, Activo, FecRecibo, NombreRecibo, idRecibo FROM Recibos WHERE idUsuario = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ReciboDTO recibo = new ReciboDTO
                        {
                            _dineroRecibo = reader.GetDecimal(0),
                            _activa = reader.GetBoolean(1),
                            _fecRecibo = reader.GetDateTime(2),
                            _nombreRecibo = reader.GetString(3),
                            _idRecibo = reader.GetInt32(4)
                        };

                        recibos.Add(recibo);
                    }
                }
            }
        }
        return recibos;
    }

public async Task<Recibo> CreateRecibo(Recibo recibo)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = @"INSERT INTO Recibos (nombreRecibo, idUsuario, idCuenta, Dinero, Activo, FecRecibo)
                         OUTPUT Inserted.idRecibo  -- Devuelve el id recién insertado
                         VALUES (@nombreRecibo, @idUsuario, @idCuenta, @Dinero, @Activo, @FecRecibo)";

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@nombreRecibo", recibo._nombreRecibo);
            command.Parameters.AddWithValue("@idUsuario", recibo._idUsuario);
            command.Parameters.AddWithValue("@idCuenta", recibo._idCuenta);
            command.Parameters.AddWithValue("@Dinero", recibo._dineroRecibo);
            command.Parameters.AddWithValue("@Activo", recibo._activa);
            command.Parameters.AddWithValue("@FecRecibo", recibo._fecRecibo);

            
            var insertedId = await command.ExecuteScalarAsync();

            
            recibo._idRecibo = Convert.ToInt32(insertedId);
        }
    }

    return recibo;  
}
}
