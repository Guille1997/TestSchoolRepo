using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestSolution.Models;

namespace TestSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        string constr = "Data Source = DESKTOP-OMV89PB\\MSSQLSERVER2;Initial Catalog = TestSchool; User ID = admin; Password=root";
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores()
        {
            using var connection = new MySqlConnection(constr);

            List<Profesor> profesores = new List<Profesor>();
            string query = "SELECT * FROM Profesor";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            profesores.Add(new Profesor
                            {
                                IdProfesor = Convert.ToInt32(sdr["IdProfesor"]),
                                NombreProfesor = Convert.ToString(sdr["NombreProfesor"]),
                                ApellidoProfesor = Convert.ToString(sdr["ApellidoProfesor"]),
                                GeneroProfesor = Convert.ToBoolean(sdr["GeneroProfesor"]),
                            });
                        }
                    }
                    con.Close();
                }
            }
            {

            }
            return profesores;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesorById(long id)
        {
            using var connection = new MySqlConnection(constr);

            Profesor profesor = new Profesor();
            string query = "SELECT * FROM Profesor where IdProfesor='" + id + "'";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        if (sdr.HasRows)
                        {
                            profesor.IdProfesor = Convert.ToInt32(sdr["IdProfesor"]);
                            profesor.NombreProfesor = Convert.ToString(sdr["NombreProfesor"]);
                            profesor.ApellidoProfesor = Convert.ToString(sdr["ApellidoProfesor"]);
                            profesor.GeneroProfesor = Convert.ToBoolean(sdr["GeneroProfesor"]);
                        }


                    }
                    con.Close();
                }
            }

            return profesor;
        }


        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            using var connection = new MySqlConnection(constr);

            string query = "INSERT INTO Profesor(NombreProfesor, ApellidoProfesor, GeneroProfesor) VALUES ('" + profesor.NombreProfesor + "', '" + profesor.ApellidoProfesor + "', '" + profesor.GeneroProfesor + "')";
            using (SqlConnection con = new SqlConnection(constr))
            {
                System.Diagnostics.Debug.WriteLine(query);
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                    }
                    con.Close();
                }
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Profesor>> PutProfesor(Profesor profesor, int id)
        {
            using var connection = new MySqlConnection(constr);

            string query = "UPDATE Profesor SET NombreProfesor='" + profesor.NombreProfesor + "', ApellidoProfesor='" + profesor.ApellidoProfesor + "', GeneroProfesor = '" + profesor.GeneroProfesor + "' WHERE IdProfesor = " + id + "";

            //string query = "UPDATE INTO Alumno(NombreAlumno, ApellidoAlumno, GeneroAlumno, FechaNac) VALUES ('" + alumno.NombreAlumno + "', '" + alumno.ApellidoAlumno + "', '" + alumno.GeneroAlumno + "','" + alumno.FechaNac.ToString("yyyy-MM-dd") + "')";
            using (SqlConnection con = new SqlConnection(constr))
            {
                System.Diagnostics.Debug.WriteLine(query);
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                    }
                    con.Close();
                }
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteProfesor(long id)
        {
            using var connection = new MySqlConnection(constr);
            string query = "Delete FROM Profesor where IdProfesor='" + id + "'";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        return NoContent();
                    }

                    con.Close();
                }
            }

            return BadRequest();
        }


    }
}
