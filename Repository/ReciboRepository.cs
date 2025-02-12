using Models;
using Microsoft.Data.SqlClient;

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

            string query = "SELECT idRecibo, idUsuario, idCuenta, Dinero, Activo, Fec_Creacion FROM Recibos";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Recibo recibo = new Recibo
                        {
                           _idRecibo = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _idCuenta= reader.GetInt32(2),
                            _dineroRecibo= reader.GetDecimal(3),
                            _activa= reader.GetBoolean(4),
                            _fec_Creacion = reader.GetDateTime(5)
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

            string query = "SELECT idRecibo, idUsuario, idCuenta, Dinero, Activo, Fec_Creacion FROM Recibos WHERE idRecibo = @idRecibo";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idRecibo", idRecibo);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        recibo = new Recibo
                        {
                         _idRecibo = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _idCuenta= reader.GetInt32(2),
                            _dineroRecibo= reader.GetDecimal(3),
                            _activa= reader.GetBoolean(4),
                            _fec_Creacion = reader.GetDateTime(5)
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

            string query = "INSERT INTO Recibos (idUsuario, idCuenta, Dinero, Activo, Fec_Creacion) VALUES (@idUsuario, @idCuenta, @Dinero, @Activo, @Fec_Creacion)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID_Usuario", recibo._idUsuario);
                command.Parameters.AddWithValue("@ID_Cuenta", recibo._idCuenta);
                command.Parameters.AddWithValue("@Dinero", recibo._dineroRecibo);
                command.Parameters.AddWithValue("@Activo", recibo._activa);
                command.Parameters.AddWithValue("@Fec_Creacion", recibo._fec_Creacion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Recibo recibo)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Recibos SET idUsuario = @idUsuario, idCuenta = @idCuenta, Dinero = @Dinero, Activo = @Activo, Fec_Creacion = @Fec_Creacion WHERE idRecibo = @idRecibo";
            using (var command = new SqlCommand(query, connection))
            {
                 command.Parameters.AddWithValue("@ID_Usuario", recibo._idUsuario);
                command.Parameters.AddWithValue("@ID_Cuenta", recibo._idCuenta);
                command.Parameters.AddWithValue("@Dinero", recibo._dineroRecibo);
                command.Parameters.AddWithValue("@Activo", recibo._activa);
                command.Parameters.AddWithValue("@Fec_Creacion", recibo._fec_Creacion);
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
}
