using System;
using System.Collections.Generic;

namespace Datos;

public partial class Alumno
{
    public int IdAlumno { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<AlumnoMaterium> AlumnoMateria { get; } = new List<AlumnoMaterium>();
}
