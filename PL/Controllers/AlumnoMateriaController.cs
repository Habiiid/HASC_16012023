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
            Modelo.AlumnoMateria alumnoMateria = new Modelo.AlumnoMateria(); //se instacio la clase AlumnoMateria

            Modelo.Result result = Negocio.AlumnoMateria.GetAllMateriasAsginadas(IdAlumno); //mandamos a llamar el metodo que muestra las materias del alumno

            alumnoMateria.Alumno = new Modelo.Alumno();  //instaciamos la clase del alumno

            Modelo.Result result1 = Negocio.Alumno.MostrarUnAlumno(IdAlumno);

            if (result.Correct && result1.Correct) //validamos que ambas listas vengan con info
            {
                alumnoMateria.AlumnosMaterias = result.Objects;
                alumnoMateria.Alumno = (Modelo.Alumno)result1.Object;
            }
            else
            {
                ViewBag.Message = "Ocurrio un error";
            }
            return View(alumnoMateria);
        }

    }
}
