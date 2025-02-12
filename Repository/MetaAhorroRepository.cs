using Models;
using Microsoft.Data.SqlClient;

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

            string query = "SELECT idMeta, idUsuario, Nombre, DescripcionMeta, DineroObjetivo, DineroActual, Activo, Fec_CreacionMeta, Fec_ObjetivoMeta FROM MetaAhorro";

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
                            _dineroObjetivo = reader.GetInt32(4),
                            _dineroActual = reader.GetInt32(5),
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

            string query = "SELECT idMeta, idUsuario, Nombre, DescripcionMeta, DineroObjetivo, DineroActual, Activo, Fec_CreacionMeta, Fec_ObjetivoMeta FROM MetaAhorro WHERE idMeta = @idMeta";

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
                            _dineroObjetivo = reader.GetInt32(4),
                            _dineroActual = reader.GetInt32(5),
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

            string query = "INSERT INTO MetaAhorro (idUsuario, Nombre, DescripcionMeta, DineroObjetivo, DineroActual, Activo, Fec_CreacionMeta, Fec_ObjetivoMeta) VALUES (@idUsuario, @Nombre, @Descripcion, @DineroObjetivo, @DineroActual, @Activo, @Fec_CreacionMeta, @Fec_ObjetivoMeta)";
            
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

            string query = "UPDATE MetaAhorro SET Nombre = @Nombre, Descripcion = @Descripcion, DineroObjetivo = @DineroObjetivo, DineroActual = @DineroActual, Activo = @Activo, Fec_ObjetivoMeta = @Fec_ObjetivoMeta WHERE idMeta = @idMeta";
            
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nombre", meta._nombreMeta);
                command.Parameters.AddWithValue("@Descripcion", meta._descripcionMeta);
                command.Parameters.AddWithValue("@DineroObjetivo", meta._dineroObjetivo);
                command.Parameters.AddWithValue("@DineroActual", meta._dineroActual);
                command.Parameters.AddWithValue("@Activo", meta._activoMeta);
                command.Parameters.AddWithValue("@Fec_ObjetivoMeta", meta._fechaObjetivoMeta);
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
}
