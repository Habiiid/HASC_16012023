using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoMateriaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Modelo.Alumno alumno = new Modelo.Alumno();
            Modelo.Result result = new Modelo.Result();

            result = Negocio.Alumno.MostrarTodosLosAlumnos(alumno);

            if (result.Correct)
            {
                alumno.Alumnos = result.Objects;
                return View(alumno);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al mostrar los alumnos.";
                return View();
            }


        }

        [HttpGet]
        public ActionResult MateriasAlumno(int IdAlumno)
        {
            //Asignadas
            Modelo.AlumnoMateria alumnoMateria = new Modelo.AlumnoMateria(); 

            Modelo.Result asignadas = Negocio.AlumnoMateria.GetAllMateriasAsginadas(IdAlumno);

            //sinasignar
            Modelo.AlumnoMateria alumnoMateria1 = new Modelo.AlumnoMateria();

            Modelo.Result noasignadas = Negocio.AlumnoMateria.MateriasNoAsignadas(IdAlumno);

            //alumno
            alumnoMateria.Alumno = new Modelo.Alumno();  

            Modelo.Result alumno = Negocio.Alumno.MostrarUnAlumno(IdAlumno);

            if (asignadas.Correct && noasignadas.Correct && alumno.Correct) 
            {
                alumnoMateria.AlumnosMaterias = asignadas.Objects; 

                alumnoMateria.Materiasasignadas = noasignadas.Objects;
               
                alumnoMateria.Alumno = (Modelo.Alumno)alumno.Object;      
                
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
            }
            return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult Agregar(Modelo.AlumnoMateria alumnomateria1)
        {
            Modelo.Result result = new Modelo.Result();

            if (alumnomateria1.Materiasasignadas != null)
            {
                foreach (string IdMateria in alumnomateria1.AlumnosMaterias)
                {
                    Modelo.AlumnoMateria alumnoMateriaAgregar = new Modelo.AlumnoMateria();

                    alumnoMateriaAgregar.Alumno = new Modelo.Alumno();
                    alumnoMateriaAgregar.Alumno.IdAlumno = alumnomateria1.Alumno.IdAlumno;

                    alumnoMateriaAgregar.Materia = new Modelo.Materia();
                    alumnoMateriaAgregar.Materia.IdMateria = int.Parse(IdMateria);

                    result = Negocio.AlumnoMateria.AgregarAlumnoMateria(alumnoMateriaAgregar);
                }


                result.Correct = true;

            }
            else
            {
                result.Correct = false;
            }
            return PartialView("Modal", alumnomateria1.Alumno);
        }

        public ActionResult Eliminar(int IdAlumnoMateria, int IdAlumno)
        {
            Modelo.AlumnoMateria alumnomateria = new Modelo.AlumnoMateria();
            alumnomateria.IdAlumnoMateria = IdAlumnoMateria;
            Modelo.Result result = Negocio.AlumnoMateria.EliminarAlumnoMateria(alumnomateria);

            ViewBag.MateriasAsignadas = true;
            ViewBag.IdAlumno = IdAlumno;

            if (result.Correct)
            {
                ViewBag.message = "Se ha eliminado exitosamente el registro";
            }
            else
            {
                ViewBag.message = "ocurrió un error al eliminar el registro " + result.ErrorMessage;

            }
            return PartialView("Modal");
        }

    }
}
