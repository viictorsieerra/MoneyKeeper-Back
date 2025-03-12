using Models;
using Microsoft.Data.SqlClient;
using DTO;

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

            string query = "SELECT idCuenta, idUsuario, Dinero, Activo, FecCreacion, Nombre FROM Cuenta";

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
                            _fechaCreacion = reader.GetDateTime(4),
                            _nombreCuenta = reader.GetString(5)
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

            string query = "SELECT idCuenta, idUsuario, Dinero, Activo, FecCreacion, Nombre FROM Cuenta WHERE idCuenta = @idCuenta";

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
                            _fechaCreacion = reader.GetDateTime(4),
                            _nombreCuenta = reader.GetString(5)
                        };
                    }
                }
            }
        }


        return cuenta;
    }

    public async Task<Cuenta> CreateCuenta(Cuenta cuenta)
{
    using (SqlConnection connection = new SqlConnection(_connectionString))
    {
        await connection.OpenAsync();

        string query = "INSERT INTO Cuenta (idUsuario, Dinero, Activo, FecCreacion, Nombre) OUTPUT INSERTED.idCuenta, INSERTED.idUsuario, INSERTED.Dinero, INSERTED.Activo, INSERTED.FecCreacion, INSERTED.Nombre VALUES (@idUsuario, @Dinero, @Activo, @FecCreacion, @Nombre)";

        using (var command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@idUsuario", cuenta._idUsuario);
            command.Parameters.AddWithValue("@Dinero", cuenta._dineroCuenta);
            command.Parameters.AddWithValue("@Activo", cuenta._activa);
            command.Parameters.AddWithValue("@FecCreacion", cuenta._fechaCreacion);
            command.Parameters.AddWithValue("@Nombre", cuenta._nombreCuenta);

            using (SqlDataReader reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    cuenta._idCuenta = reader.GetInt32(0); // Asignamos el id generado
                    cuenta._idUsuario = reader.GetInt32(1);
                    cuenta._dineroCuenta = reader.GetDecimal(2);
                    cuenta._activa = reader.GetBoolean(3);
                    cuenta._fechaCreacion = reader.GetDateTime(4);
                    cuenta._nombreCuenta = reader.GetString(5);
                }
            }
        }
    }
    return cuenta; 
}

    public async Task UpdateCuenta(Cuenta cuenta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Cuenta SET Dinero = @Dinero, Activo = @Activo, FecCreacion = @FecCreacion, Nombre = @Nombre WHERE idCuenta = @idCuenta";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCuenta", cuenta._idCuenta);
                command.Parameters.AddWithValue("@Dinero", cuenta._dineroCuenta);
                command.Parameters.AddWithValue("@Activo", cuenta._activa);
                command.Parameters.AddWithValue("@FecCreacion", cuenta._fechaCreacion);
                command.Parameters.AddWithValue("@Nombre",cuenta._nombreCuenta );

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsyncById(int idCuenta)
    {

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Cuenta WHERE idCuenta = @idCuenta";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idCuenta", idCuenta);

                await command.ExecuteNonQueryAsync();
            }
        }
        
    }

        public async Task InicializarDatosAsync()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"INSERT INTO Cuenta (IdUsuario, Dinero) VALUES (3, 5000.00), (4, 3200.50);";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }
public async Task<List<CuentaDTO>> GetByUser(string idUsuario)
    {
        List<CuentaDTO> cuentas = new List<CuentaDTO>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

             string query = "SELECT CU.idCuenta, CU.dinero, CU.activo, CU.fecCreacion, CU.nombre FROM Cuenta CU\n" + 
                            "INNER JOIN Usuario us ON CU.idUsuario = US.idUsuario\n";
 
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        CuentaDTO transaccion = new CuentaDTO
                        {
                            _idCuenta = reader.GetInt32(0),
                            _dineroCuenta = reader.GetDecimal(1),
                            _activa = reader.GetBoolean(2),
                            _fechaCreacion = reader.GetDateTime(3),
                            _nombreCuenta = reader.GetString(4)
                        };

                        cuentas.Add(transaccion);
                    }
                }
            }
        }
        return cuentas;
    }

}