using Models;
using Microsoft.Data.SqlClient;
using DTO;

namespace Repositories;

class GastoRepository : IGastoRepository
{
    private readonly string? _connectionString;

    public GastoRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Gasto>> GetAllAsync()
    {
        List<Gasto> gastos = new List<Gasto>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdGasto ,IdPresupuesto ,Nombre ,Descripcion ,Cantidad ,FecCreacion FROM Gastos";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Gasto gasto = new Gasto
                        {
                            _idGasto = reader.GetInt32(0),
                            _idPresupuesto = reader.GetInt32(1),
                            _nombre = reader.GetString(2),
                            _descripcion = reader.GetString(3),
                            _cantidad = reader.GetDecimal(4),
                            _fecCreacion = reader.GetDateTime(5)
                        };

                        gastos.Add(gasto);
                    }
                }
            }
        }
        return gastos;
    }

    public async Task<Gasto> GetByIdAsync(int id)
    {
        Gasto gasto = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdGasto ,IdPresupuesto ,Nombre ,Descripcion ,Cantidad ,FecCreacion FROM Gastos WHERE IdGasto = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        gasto = new Gasto
                        {
                            _idGasto = reader.GetInt32(0),
                            _idPresupuesto = reader.GetInt32(1),
                            _nombre = reader.GetString(2),
                            _descripcion = reader.GetString(3),
                            _cantidad = reader.GetDecimal(4),
                            _fecCreacion = reader.GetDateTime(5)
                        };
                    }
                }
            }
        }

        return gasto;
    }

    public async Task<List<Gasto>> GetByPresupuestoAsync(int idPresupuesto)
    {
        List<Gasto> gastos = new List<Gasto>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdGasto ,IdPresupuesto , gas.Nombre ,Descripcion ,Cantidad ,gas.FecCreacion FROM Gastos WHERE IdPresupuesto = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idPresupuesto);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Gasto gasto = new Gasto
                        {
                            _idGasto = reader.GetInt32(0),
                            _idPresupuesto = reader.GetInt32(1),
                            _nombre = reader.GetString(2),
                            _descripcion = reader.GetString(3),
                            _cantidad = reader.GetDecimal(4),
                            _fecCreacion = reader.GetDateTime(5)
                        };

                        gastos.Add(gasto);
                    }
                }
            }
        }
        return gastos;
    }

    public async Task AddAsync(Gasto gasto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Gastos (IdPresupuesto, Nombre, Descripcion, Cantidad, FecCreacion) VALUES (@IdPresupuesto, @Nombre, @Descripcion, @Cantidad, @FecCreacion)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdPresupuesto", gasto._idPresupuesto);
                command.Parameters.AddWithValue("@Nombre", gasto._nombre);
                command.Parameters.AddWithValue("@Descripcion", gasto._descripcion);
                command.Parameters.AddWithValue("@Cantidad", gasto._cantidad);
                command.Parameters.AddWithValue("@FecCreacion", gasto._fecCreacion);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Gasto gasto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Gastos SET IdPresupuesto = @IdPresupuesto, Nombre = @Nombre, Descripcion = @Descripcion, Cantidad = @Cantidad, FecCreacion = @FecCreacion WHERE IdGasto = @IdGasto";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdPresupuesto", gasto._idPresupuesto);
                command.Parameters.AddWithValue("@Nombre", gasto._nombre);
                command.Parameters.AddWithValue("@Descripcion", gasto._descripcion);
                command.Parameters.AddWithValue("@Cantidad", gasto._cantidad);
                command.Parameters.AddWithValue("@FecCreacion", gasto._fecCreacion);
                command.Parameters.AddWithValue("@IdGasto", gasto._idGasto);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idGasto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM Gastos WHERE IdGasto = @IdGasto";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdGasto", idGasto);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
