using System;
using System.Collections.Generic;

namespace Datos;

public partial class Materium
{
    public int IdMateria { get; set; }

    public string? Nombre { get; set; }

    public decimal? Costo { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<AlumnoMaterium> AlumnoMateria { get; } = new List<AlumnoMaterium>();
}
