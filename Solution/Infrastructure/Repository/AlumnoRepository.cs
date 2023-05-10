using Core.Entity;
using Infrastructure.Configuration;
using Infrastructure.Repository.Interfaces;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class AlumnoRepository: IAlumnoRepository
    {
        private readonly Conexion _conexion;
        public AlumnoRepository(IOptions<Conexion> conexion)
        {
            _conexion = conexion.Value;
        }



        public async Task<List<Alumno>> GetAll()
        {
            List<Alumno> lista = new List<Alumno>();

            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Alumno_All", conexion);
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.StoredProcedure;

            using var dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                lista.Add(new Alumno
                {
                    IdAlumno = Convert.ToInt32(dr["IdAlumno"]),
                    Nombres = dr["Nombres"].ToString(),
                    ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                    ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                    DNI = dr["DNI"].ToString(),
                    Direccion = dr["Direccion"].ToString(),
                    Telefono = dr["Telefono"].ToString()
                });
            }

            return lista;
        }

        public async Task<Alumno> GetById(int id)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Alumno_ById", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdAlumno", id);

            var dr = await cmd.ExecuteReaderAsync();
            await dr.ReadAsync();

            return new Alumno
            {
                IdAlumno = Convert.ToInt32(dr["IdAlumno"]),
                Nombres = dr["Nombres"].ToString(),
                ApellidoPaterno = dr["ApellidoPaterno"].ToString(),
                ApellidoMaterno = dr["ApellidoMaterno"].ToString(),
                DNI = dr["DNI"].ToString(),
                Direccion = dr["Direccion"].ToString(),
                Telefono = dr["Telefono"].ToString()
            };
        }

        public async Task Post(Alumno entidad)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Alumno_Add", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Nombres", entidad.Nombres);
            cmd.Parameters.AddWithValue("@ApellidoPaterno", entidad.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@ApellidoMaterno", entidad.ApellidoMaterno);
            cmd.Parameters.AddWithValue("@DNI", entidad.DNI);
            cmd.Parameters.AddWithValue("@Direccion", entidad.Direccion);
            cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);

            var newId = await cmd.ExecuteScalarAsync();
        }

        public async Task Update(Alumno entidad)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Alumno_Update", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdAlumno", entidad.IdAlumno);
            cmd.Parameters.AddWithValue("@Nombres", entidad.Nombres);
            cmd.Parameters.AddWithValue("@ApellidoPaterno", entidad.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@ApellidoMaterno", entidad.ApellidoMaterno);
            cmd.Parameters.AddWithValue("@DNI", entidad.DNI);
            cmd.Parameters.AddWithValue("@Direccion", entidad.Direccion);
            cmd.Parameters.AddWithValue("@Telefono", entidad.Telefono);

            var newId = await cmd.ExecuteScalarAsync();
        }

        public async Task Delete(int id)
        {
            using var conexion = new SqlConnection(_conexion.CadenaSQL);
            await conexion.OpenAsync();

            SqlCommand cmd = new SqlCommand("sp_Alumno_Delete", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdAlumno", id);

            await cmd.ExecuteNonQueryAsync();
        }
    }
}
