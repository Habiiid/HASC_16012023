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
        public ActionResult Form(int IdAlumno)
        {
            Modelo.Result result = Negocio.AlumnoMateria.MateriasNoAsignadas(IdAlumno);
            Modelo.AlumnoMateria alumnomateria = new Modelo.AlumnoMateria();

            Modelo.Result resultalumno = Negocio.Alumno.MostrarUnAlumno(IdAlumno);

            alumnomateria.AlumnosMaterias = result.Objects;
            alumnomateria.Alumno = ((Modelo.Alumno)resultalumno.Object);

            return View(alumnomateria);
        }

    }
}
