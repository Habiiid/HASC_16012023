using System;
using System.Collections.Generic;

namespace Datos;

public partial class AlumnoMaterium
{
    public int IdAlumnoMateria { get; set; }

    public int? IdAlumno { get; set; }

    public int? IdMateria { get; set; }

    public virtual Alumno? IdAlumnoNavigation { get; set; }

    public virtual Materium? IdMateriaNavigation { get; set; }

    //agregadas

    public string NombreAlumno { get; set; }
    public string NombreMateria { get; set; }
    public string ApellidoPaterno { get; set; }
    public string ApellidoMaterno { get; set; }
    public decimal Costo { get; set; }

    //public string Nombre { get; set; }
}
