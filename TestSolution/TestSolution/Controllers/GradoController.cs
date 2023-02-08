using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
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
    public class GradoController : ControllerBase
    {
        string constr = "Data Source = DESKTOP-OMV89PB\\MSSQLSERVER2;Initial Catalog = TestSchool; User ID = admin; Password=root";
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grado>>> GetGrados()
        {
            using var connection = new MySqlConnection(constr);

            List<Grado> grados = new List<Grado>();
            string query = "SELECT * FROM Grado";
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
                            grados.Add(new Grado
                            {
                                IdGrado = Convert.ToInt32(sdr["IdGrado"]),
                                NombreGrado = Convert.ToString(sdr["NombreGrado"]),
                                IdProfesor = Convert.ToInt32(sdr["IdProfesor"]),

                            });
                        }
                    }
                    con.Close();
                }
            }
            {

            }
            return grados;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Grado>> GetGradoById(long id)
        {
            using var connection = new MySqlConnection(constr);

            Grado grado = new Grado();
            string query = "SELECT * FROM Grado where IdGrado='" + id + "'";
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
                            grado.IdGrado = Convert.ToInt32(sdr["IdGrado"]);
                            grado.NombreGrado = Convert.ToString(sdr["NombreGrado"]);
                            grado.IdProfesor = Convert.ToInt32(sdr["IdProfesor"]);
                        }


                    }
                    con.Close();
                }
            }

            return grado;
        }


        [HttpPost]
        public async Task<ActionResult<Grado>> PostGrado(Grado grado)
        {
            using var connection = new MySqlConnection(constr);

            string query = "INSERT INTO Grado (NombreGrado, IdProfesor) VALUES ('" + grado.NombreGrado + "', '" + grado.IdProfesor + "')";
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
        public async Task<ActionResult<Grado>> PutGrado(Grado grado, long id)
        {
            using var connection = new MySqlConnection(constr);

            string query = "UPDATE Grado SET NombreGrado='" + grado.NombreGrado + "', IdProfesor='" + grado.IdProfesor +"' WHERE IdGrado = " + id + "";

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
        public async Task<IActionResult> deleteGrado(long id)
        {
            using var connection = new MySqlConnection(constr);
            string query = "Delete FROM Grado where IdGrado='" + id + "'";
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
