using Models;
using Microsoft.Data.SqlClient;

namespace Repositories;

public class CuentaRepository : ICuentaRepository
{
    private readonly string? _connectionString;

    public CuentaRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Cuenta>> GetAllAsync()
    {
        List<Cuenta> cuentas = new List<Cuenta>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idCuenta, idUsuario, Dinero, Activo, FecCreacion FROM Cuentas";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Cuenta cuenta = new Cuenta
                        {
                            _idCuenta = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _dineroCuenta = reader.GetDecimal(2),
                            _activa = reader.GetBoolean(3),
                            _fechaCreacion = reader.GetDateTime(4)
                        };

                        cuentas.Add(cuenta);
                    }
                }
            }
        }
        return cuentas;
    }

    public async Task<Cuenta> GetByIdAsync(int idUsuario)
    {
        Cuenta cuenta = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idCuenta, idUsuario, Dinero, Activo, FecCreacion FROM Cuentas WHERE idCuenta = @idCuenta";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCuenta", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        cuenta = new Cuenta
                        {
                            _idCuenta = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _dineroCuenta = reader.GetDecimal(2),
                            _activa = reader.GetBoolean(3),
                            _fechaCreacion = reader.GetDateTime(4)
                        };
                    }
                }
            }
        }


        return cuenta;
    }

    public async Task AddAsync(Cuenta cuenta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Cuentas (idUsuario, Dinero, Activo, FecCreacion) VALUES (@idUsuario, @Dinero, @Activo, @FecCreacion)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idUsuario", cuenta._idUsuario);
                command.Parameters.AddWithValue("@Dinero", cuenta._dineroCuenta);
                command.Parameters.AddWithValue("@Activo", cuenta._activa);
                command.Parameters.AddWithValue("@FechaCreacion", cuenta._fechaCreacion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Cuenta cuenta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Cuentas SET idUsuario = @idUsuario , Dinero = @Dinero, Activo = @Activo, FecCreacion = @FecCreacion WHERE idCuenta = @idCuenta";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCuenta", cuenta._idCuenta);
                command.Parameters.AddWithValue("@idUsuario", cuenta._idUsuario);
                command.Parameters.AddWithValue("@Dinero", cuenta._dineroCuenta);
                command.Parameters.AddWithValue("@Activo", cuenta._activa);
                command.Parameters.AddWithValue("@FechaCreacion", cuenta._fechaCreacion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idCuenta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Cuentas WHERE idCuenta = @idCuenta";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCuenta", idCuenta);

                await command.ExecuteNonQueryAsync();
            }
        }
    }


}