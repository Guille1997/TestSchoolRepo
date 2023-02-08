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
    public class AlumnoGradoController : ControllerBase
    {
        string constr = "Data Source = DESKTOP-OMV89PB\\MSSQLSERVER2;Initial Catalog = TestSchool; User ID = admin; Password=root";
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnoGrado>>> GetAlumnoGrados()
        {
            using var connection = new MySqlConnection(constr);

            List<AlumnoGrado> alumnoGrados = new List<AlumnoGrado>();
            string query = "SELECT * FROM AlumnoGrado";
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
                            alumnoGrados.Add(new AlumnoGrado
                            {
                                IdAlumnoGrado = Convert.ToInt32(sdr["IdAlumnoGrado"]),
                                IdAlumno = Convert.ToInt32(sdr["IdAlumno"]),
                                IdGrado = Convert.ToInt32(sdr["IdGrado"]),
                                Seccion = Convert.ToString(sdr["Seccion"])

                            });
                        }
                    }
                    con.Close();
                }
            }
            {

            }
            return alumnoGrados;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlumnoGrado>> GetAlumnoGradoById(long id)
        {
            using var connection = new MySqlConnection(constr);

            AlumnoGrado alumnoGrado = new AlumnoGrado();
            string query = "SELECT * FROM AlumnoGrado where IdAlumnoGrado='" + id + "'";
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
                            alumnoGrado.IdAlumnoGrado = Convert.ToInt32(sdr["IdAlumnoGrado"]);
                            alumnoGrado.IdAlumno = Convert.ToInt32(sdr["IdAlumno"]);
                            alumnoGrado.IdGrado = Convert.ToInt32(sdr["IdGrado"]);
                            alumnoGrado.Seccion = Convert.ToString(sdr["Seccion"]);
                        }


                    }
                    con.Close();
                }
            }

            return alumnoGrado;
        }

        [HttpPost]
        public async Task<ActionResult<AlumnoGrado>> PostAlumnoGrado(AlumnoGrado alumnoGrado)
        {
            using var connection = new MySqlConnection(constr);

            string query = "INSERT INTO AlumnoGrado (IdAlumno, IdGrado, Seccion) VALUES ('" + alumnoGrado.IdAlumno + "', '" + alumnoGrado.IdGrado + "' , '" + alumnoGrado.Seccion + "' )";
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
        public async Task<ActionResult<AlumnoGrado>> PutGrado(AlumnoGrado alumnoGrado, int id)
        {
            using var connection = new MySqlConnection(constr);

            string query = "UPDATE AlumnoGrado SET IdAlumno='" + alumnoGrado.IdAlumno + "', IdGrado='" + alumnoGrado.IdGrado + "', Seccion = '" + alumnoGrado.Seccion  + "' WHERE IdAlumnoGrado = " + id + "";

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
        public async Task<IActionResult> deleteAlumnoGrado(long id)
        {
            using var connection = new MySqlConnection(constr);
            string query = "Delete FROM AlumnoGrado where IdGrado='" + id + "'";
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
