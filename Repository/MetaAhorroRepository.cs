using Models;
using Microsoft.Data.SqlClient;
using DTO;

namespace Repositories;

class MetaAhorroRepository : IMetaAhorroRepository
{
    private readonly string? _connectionString;

    public MetaAhorroRepository(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<List<MetaAhorro>> GetAllAsync()
    {
        List<MetaAhorro> metas = new List<MetaAhorro>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idMeta, idUsuario, Nombre, Descripcion, DineroObjetivo, DineroActual, Activo, FecCreacion, FecObjetivo FROM MetaAhorro";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        MetaAhorro meta = new MetaAhorro
                        {
                            _idMeta = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _nombreMeta = reader.GetString(2),
                            _descripcionMeta = reader.GetString(3),
                            _dineroObjetivo = reader.GetDecimal(4),
                            _dineroActual = reader.GetDecimal(5),
                            _activoMeta = reader.GetBoolean(6),
                            _fechaCreacionMeta = reader.GetDateTime(7),
                            _fechaObjetivoMeta = reader.GetDateTime(8)
                        };

                        metas.Add(meta);
                    }
                }
            }
        }
        return metas;
    }

    public async Task<MetaAhorro> GetByIdAsync(int idMeta)
    {
        MetaAhorro meta = null;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "SELECT idMeta, idUsuario, Nombre, Descripcion, DineroObjetivo, DineroActual, Activo, FecCreacion, FecObjetivo FROM MetaAhorro WHERE idMeta = @idMeta";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idMeta", idMeta);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        meta = new MetaAhorro
                        {
                            _idMeta = reader.GetInt32(0),
                            _idUsuario = reader.GetInt32(1),
                            _nombreMeta = reader.GetString(2),
                            _descripcionMeta = reader.GetString(3),
                            _dineroObjetivo = reader.GetDecimal(4),
                            _dineroActual = reader.GetDecimal(5),
                            _activoMeta = reader.GetBoolean(6),
                            _fechaCreacionMeta = reader.GetDateTime(7),
                            _fechaObjetivoMeta = reader.GetDateTime(8)
                        };
                    }
                }
            }
        }
        return meta;
    }

    public async Task AddAsync(MetaAhorro meta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "INSERT INTO MetaAhorro (idUsuario, Nombre, Descripcion, DineroObjetivo, DineroActual, Activo, FecCreacion, FecObjetivo) VALUES (@idUsuario, @Nombre, @Descripcion, @DineroObjetivo, @DineroActual, @Activo, @FecCreacion, @FecObjetivo)";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@ID_Usuario_FK", meta._idMeta);
                command.Parameters.AddWithValue("@Nombre", meta._nombreMeta);
                command.Parameters.AddWithValue("@Descripcion", meta._descripcionMeta);
                command.Parameters.AddWithValue("@DineroObjetivo", meta._dineroObjetivo);
                command.Parameters.AddWithValue("@DineroActual", meta._dineroActual);
                command.Parameters.AddWithValue("@Activo", meta._activoMeta);
                command.Parameters.AddWithValue("@Fec_CreacionMeta", meta._fechaCreacionMeta);
                command.Parameters.AddWithValue("@Fec_ObjetivoMeta", meta._fechaObjetivoMeta);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateAsync(MetaAhorro meta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "UPDATE MetaAhorro SET Nombre = @Nombre, Descripcion = @Descripcion, DineroObjetivo = @DineroObjetivo, DineroActual = @DineroActual, Activo = @Activo, FecObjetivo = @FecObjetivo WHERE idMeta = @idMeta";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", meta._nombreMeta);
                command.Parameters.AddWithValue("@Descripcion", meta._descripcionMeta);
                command.Parameters.AddWithValue("@DineroObjetivo", meta._dineroObjetivo);
                command.Parameters.AddWithValue("@DineroActual", meta._dineroActual);
                command.Parameters.AddWithValue("@Activo", meta._activoMeta);
                command.Parameters.AddWithValue("@FecObjetivo", meta._fechaObjetivoMeta);
                command.Parameters.AddWithValue("@idMeta", meta._idMeta);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int idMeta)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = "DELETE FROM MetaAhorro WHERE idMeta = @idMeta";

            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idMeta", idMeta);

                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task InicializarDatosAsync()
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

            string query = @"INSERT INTO MetaAhorro (IdUsuario, Nombre, Descripcion, DineroObjetivo, DineroActual, FecObjetivo) VALUES
                            (3, 'Mundial 2030', 'Mundial de España de fútbol 2030', 3000.00, 500.00, '2030-04-01'),
                            (4, 'Comprar un coche', 'Ahorro para un coche nuevo', 10000.00, 2000.00, '2027-06-15');";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }


    public async Task<List<MetaAhorroDTO>> GetByUser(string idUsuario)
    {
        List<MetaAhorroDTO> metas = new List<MetaAhorroDTO>();

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();

             string query = "SELECT ME.Nombre, ME.Descripcion, ME.DineroObjetivo, ME.DineroActual, ME.Activo, ME.FecCreacion, ME.FecObjetivo FROM MetaAhorro ME\n" +
            "INNER JOIN Usuario us ON ME.idUsuario = US.idUsuario\n WHERE Us.idUsuario = @id";
 
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", idUsuario);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        MetaAhorroDTO meta = new MetaAhorroDTO
                        {
                         
                            _nombreMeta = reader.GetString(0),
                            _descripcionMeta = reader.GetString(1),
                            _dineroObjetivo = reader.GetDecimal(2),
                            _dineroActual = reader.GetDecimal(3),
                            _activoMeta = reader.GetBoolean(4),
                            _fechaCreacionMeta = reader.GetDateTime(5),
                            _fechaObjetivoMeta = reader.GetDateTime(6)
                        };

                        metas.Add(meta);
                    }
                }
            }
        }
        return metas;
    }


}
