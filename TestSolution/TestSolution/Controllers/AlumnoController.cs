using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestSolution.Models;

namespace TestSolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        string constr = "Data Source = DESKTOP-OMV89PB\\MSSQLSERVER2;Initial Catalog = TestSchool; User ID = admin; Password=root";
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> GetAlumnos()
        {
            using var connection = new MySqlConnection(constr);

            List<Alumno> alumnos = new List<Alumno>();
            string query = "SELECT * FROM Alumno";
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
                            alumnos.Add(new Alumno
                            {
                                IdAlumno = Convert.ToInt32(sdr["IdAlumno"]),
                                NombreAlumno = Convert.ToString(sdr["NombreAlumno"]),
                                ApellidoAlumno = Convert.ToString(sdr["ApellidoAlumno"]),
                                GeneroAlumno = Convert.ToBoolean(sdr["GeneroAlumno"]),
                                FechaNac = Convert.ToDateTime(sdr["FechaNac"])
                            });
                        }
                    }
                    con.Close();
                }
            }
            {

            }
            return alumnos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alumno>> GetAlumnoById(long id)
        {
            using var connection = new MySqlConnection(constr);

            Alumno alumno = new Alumno();
            string query = "SELECT * FROM Alumno where IdAlumno='" + id + "'";
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
                            alumno.IdAlumno = Convert.ToInt32(sdr["IdAlumno"]);
                            alumno.NombreAlumno = Convert.ToString(sdr["NombreAlumno"]);
                            alumno.ApellidoAlumno = Convert.ToString(sdr["ApellidoAlumno"]);
                            alumno.GeneroAlumno = Convert.ToBoolean(sdr["GeneroAlumno"]);
                            alumno.FechaNac = Convert.ToDateTime(sdr["FechaNac"]);
                        }
                            
                       
                    }
                    con.Close();
                }
            }
            
            return alumno;
        }


        [HttpPost]
        public async Task<ActionResult<Alumno>> PostAlumno(Alumno alumno)
        {
            using var connection = new MySqlConnection(constr);

            string query = "INSERT INTO Alumno(NombreAlumno, ApellidoAlumno, GeneroAlumno, FechaNac) VALUES ('" + alumno.NombreAlumno + "', '" + alumno.ApellidoAlumno + "', '" + alumno.GeneroAlumno + "','" + alumno.FechaNac.ToString("yyyy-MM-dd") + "')";
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

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Alumno>> PutAlumno(int id,Alumno alumno)
        {
            using var connection = new MySqlConnection(constr);

            string query = "UPDATE Alumno SET NombreAlumno='" + alumno.NombreAlumno + "', ApellidoAlumno='" + alumno.ApellidoAlumno + "', GeneroAlumno = '" + alumno.GeneroAlumno + "', FechaNac = '" + alumno.FechaNac.ToString("yyyy-MM-dd") + "' WHERE IdAlumno = " + id + "";

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
        public async Task<IActionResult> deleteAlumno(long id)
        {
            using var connection = new MySqlConnection(constr);
            string query = "Delete FROM Alumno where IdAlumno='" + id + "'";
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
