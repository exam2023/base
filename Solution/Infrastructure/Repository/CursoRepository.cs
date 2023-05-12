using Core.Entity;
using Infrastructure.Configuration;
using Infrastructure.Repository.Interfaces;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repository
{
    public class CursoRepository : ICursoRepository
    {

        private readonly Conexion _conexion;

        public CursoRepository(IOptions<Conexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task Delete(int id)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Curso_delete", conexion);
            cmd.CommandType= CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCurso", id);

            await cmd.ExecuteNonQueryAsync();
             
        }

        public async Task<List<Curso>> GetAll()
        {
            List<Curso> lista = new List<Curso>();

            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Curso_All", conexion);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                lista.Add(new Curso
                {
                    IdCurso = Convert.ToInt32(dr["IdCurso"]),
                    Nombre = dr["Nombre"].ToString(),
                    Docente = dr["Docente"].ToString()
                });
            }

            return lista;
        }

        public async Task<Curso> GetById(int id)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Curso_ById", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdCurso", id);

            var dr = await cmd.ExecuteReaderAsync();
            await dr.ReadAsync();

            return new Curso
            {
                IdCurso = Convert.ToInt32(dr["IdCurso"]),
                Nombre = dr["Nombre"].ToString(),
                Docente = dr["Docente"].ToString()
            };
        }

        public async Task Post(Curso entity)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Curso_Add", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("Nombre", entity.Nombre);
            cmd.Parameters.AddWithValue("Docente", entity.Docente);

            var newId = await cmd.ExecuteScalarAsync();
        }

        public async Task Update(Curso entity)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Curso_Update", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdCurso", entity.IdCurso);
            cmd.Parameters.AddWithValue("@Nombre", entity.Nombre);
            cmd.Parameters.AddWithValue("@Docente", entity.Docente);

            await cmd.ExecuteNonQueryAsync();

        }
    }
}
