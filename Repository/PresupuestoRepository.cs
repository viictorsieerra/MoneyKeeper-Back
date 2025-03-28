using Models;
using Microsoft.Data.SqlClient;
using DTO;

namespace Repositories;

class PresupuestoRepository : IPresupuestoRepository
{
    private readonly string? _connectionString;

    public PresupuestoRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<Presupuesto>> GetAllAsync()
    {
        List<Presupuesto> presupuestos = new List<Presupuesto>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdPresupuesto ,IdUsuario ,IdCategoria ,Nombre ,Limite ,DineroActual ,FecCreacion ,Activo FROM Presupuestos";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        Presupuesto Presupuesto = new Presupuesto
                        {
                            _idPresupuesto = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _idCategoria = reader.GetInt32(2),
                            _nombre = reader.GetString(3),
                            _limite = reader.GetDecimal(4),
                            _dineroActual = reader.GetDecimal(5),
                            _fecCreacion = reader.GetDateTime(6),
                            _activo = reader.GetBoolean(7)
                        };

                        presupuestos.Add(Presupuesto);
                    }
                }
            }
        }
        return presupuestos;
    }

    public async Task<Presupuesto> GetByIdAsync(int id)
    {
        Presupuesto presupuesto = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdPresupuesto ,IdUsuario ,IdCategoria ,Nombre ,Limite ,DineroActual ,FecCreacion ,Activo FROM Presupuestos WHERE IdPresupuesto = @id";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        presupuesto = new Presupuesto
                        {
                            _idPresupuesto = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _idCategoria = reader.GetInt32(2),
                            _nombre = reader.GetString(3),
                            _limite = reader.GetDecimal(4),
                            _dineroActual = reader.GetDecimal(5),
                            _fecCreacion = reader.GetDateTime(6),
                            _activo = reader.GetBoolean(7)
                        };
                    }
                }
            }
        }

        return presupuesto;
    }

    public async Task<List<PresupuestoDTO>> GetByUser(string idUsuario)
    {
        List<PresupuestoDTO> presupuestos = new List<PresupuestoDTO>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT IdPresupuesto, IdUsuario, ca.nombre, pre.Nombre, Limite, DineroActual, FecCreacion, Activo FROM Presupuestos pre INNER JOIN Categoria ca ON pre.idCategoria = ca.IdCategoria WHERE IdUsuario = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        PresupuestoDTO presupuesto = new PresupuestoDTO
                        {
                            _idPresupuesto = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _nombreCategoria = reader.GetString(2),
                            _nombrePresupuesto = reader.GetString(3),
                            _limite = reader.GetDecimal(4),
                            _dineroActual = reader.GetDecimal(5),
                            _fecCreacion = reader.GetDateTime(6),
                            _activo = reader.GetBoolean(7)
                        };

                        presupuestos.Add(presupuesto);
                    }
                }
            }
        }

        return presupuestos;
    }


    public async Task AddAsync(Presupuesto presupuesto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO Presupuestos (IdUsuario, IdCategoria, Nombre, Limite, DineroActual, FecCreacion, Activo) VALUES (@IdUsuario, @IdCategoria, @Nombre, @Limite, @DineroActual, @FecCreacion, @Activo)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdUsuario", presupuesto._idUsuario);
                command.Parameters.AddWithValue("@IdCategoria", presupuesto._idCategoria);
                command.Parameters.AddWithValue("@Nombre", presupuesto._nombre);
                command.Parameters.AddWithValue("@Limite", presupuesto._limite);
                command.Parameters.AddWithValue("@DineroActual", presupuesto._dineroActual);
                command.Parameters.AddWithValue("@FecCreacion", presupuesto._fecCreacion);
                command.Parameters.AddWithValue("@Activo", presupuesto._activo);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(Presupuesto presupuesto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE Presupuestos SET IdUsuario = @IdUsuario, IdCategoria = @IdCategoria, Nombre = @Nombre, Limite = @Limite, DineroActual = @DineroActual, FecCreacion = @FecCreacion, Activo = @Activo WHERE IdPresupuesto = @IdPresupuesto";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdPresupuesto", presupuesto._idPresupuesto);
                command.Parameters.AddWithValue("@IdUsuario", presupuesto._idUsuario);
                command.Parameters.AddWithValue("@IdCategoria", presupuesto._idCategoria);
                command.Parameters.AddWithValue("@Nombre", presupuesto._nombre);
                command.Parameters.AddWithValue("@Limite", presupuesto._limite);
                command.Parameters.AddWithValue("@DineroActual", presupuesto._dineroActual);
                command.Parameters.AddWithValue("@FecCreacion", presupuesto._fecCreacion);
                command.Parameters.AddWithValue("@Activo", presupuesto._activo);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idPresupuesto)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM presupuestos WHERE IdPresupuesto = @IdPresupuesto";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IdPresupuesto", idPresupuesto);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
