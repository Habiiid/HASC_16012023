using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AlumnoMateria
    {

        public int IdAlumnoMateria { get; set; }
        public int IdAlumno { get; set; }
        public int IdMateria { get; set; }

        public List<Object> AlumnosMaterias { get; set; }

        public Modelo.Alumno Alumno { get; set; }
        public Modelo.Materia Materia { get; set; }
    }
}
