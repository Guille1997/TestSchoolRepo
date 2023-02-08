using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestSolution.Models
{
    public class Alumno
    {
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public string ApellidoAlumno { get; set; }
        public bool GeneroAlumno { get; set; }
        public DateTime FechaNac { get; set; }

    }
}
