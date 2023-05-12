using Core.Entity;
using Infrastructure.Configuration;
using Infrastructure.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace Infrastructure.Repository
{
    public class NotasRepository : INotasRepository
    {
        private readonly Conexion _conexion;

        public NotasRepository(IOptions<Conexion> conexion)
        {
            _conexion = conexion.Value;
        }

        public async Task Delete(int id)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Nota_Delete", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdNota", id);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<Notas>> GetAll()
        {
            List<Notas> lista = new List<Notas>();

            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Nota_All", conexion);
            cmd.CommandTimeout= 0;
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                lista.Add(new Notas
                {
                    IdNota = Convert.ToInt32(dr["IdNota"]),
                    IdAlumno = Convert.ToInt32(dr["IdAlumno"]),
                    IdCurso = Convert.ToInt32(dr["IdCurso"]),
                    Nota = Convert.ToInt32(dr["Nota"]),
                    Alumno = dr["Alumno"].ToString(),
                    Curso = dr["Curso"].ToString()
                });
            }

            return lista;

        }

        public async Task<Notas> GetById(int id)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Nota_ById",conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdNota", id);
            var dr = await cmd.ExecuteReaderAsync();
            await dr.ReadAsync();
            return new Notas
            {
                IdNota = Convert.ToInt32(dr["IdNota"]),
                IdAlumno = Convert.ToInt32(dr["IdAlumno"]),
                IdCurso = Convert.ToInt32(dr["IdCurso"]),
                Nota = Convert.ToInt32(dr["Nota"]),
                Alumno = dr["Alumno"].ToString(),
                Curso = dr["Curso"].ToString()
            };
        }

        public async Task Post(Notas entity)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Nota_Add", conexion);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@IdAlumno", entity.IdAlumno);
            cmd.Parameters.AddWithValue("@IdCurso", entity.IdCurso);
            cmd.Parameters.AddWithValue("@Nota", entity.Nota);

            var Id = await cmd.ExecuteScalarAsync();
        }

        public async Task Update(Notas entity)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Nota_Update", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdNota", entity.IdNota);
            //cmd.Parameters.AddWithValue("@IdAlumno", entity.IdAlumno);
            //cmd.Parameters.AddWithValue("@IdCurso", entity.IdCurso);
            cmd.Parameters.AddWithValue("@Nota", entity.Nota);

            await cmd.ExecuteNonQueryAsync();


        }
    }
}
