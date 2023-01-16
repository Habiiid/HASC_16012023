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


       
    }
}
