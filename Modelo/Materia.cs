using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Materia
    {
        public int? IdMateria { get; set; }
        public string? Nombre { get; set; }
        public decimal Costo { get; set; }
        public string? Descripcion { get; set; }

        //lista
        public List<Object>? Materias{ get; set; }


    }
}
