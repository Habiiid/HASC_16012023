using Microsoft.AspNetCore.Mvc;
using Modelo;

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
            decimal total = 0;
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
                ViewBag.Massage = "Ocurrio un error";
            }

           
            if (alumnoMateria.AlumnosMaterias.Count > 0)
            {
                foreach (Modelo.AlumnoMateria materiacosto in alumnoMateria.AlumnosMaterias)
                {
                    //total = total + materiacosto.Materia.Costo;
                }
            }
            ViewBag.Correct = true;
            ViewBag.Total = total;

            return View(alumnoMateria);
        }

        [HttpPost]
        public ActionResult Agregar(Modelo.AlumnoMateria alumnoMateria, List<int> Materiasasignadas)
        {
            Modelo.Result result1 = new Modelo.Result();
       

            if (alumnoMateria.Materiasasignadas != null)
            {
                foreach(int IdMateria in Materiasasignadas)
                {
                   // Modelo.AlumnoMateria alumnoMateriaAgregar = new Modelo.AlumnoMateria();

                    //alumnoMateriaAgregar.Alumno = new Modelo.Alumno();
                    alumnoMateria.IdAlumno = alumnoMateria.Alumno.IdAlumno;

                    //alumnoMateriaAgregar.Materia = new Modelo.Materia();
                    alumnoMateria.IdMateria = IdMateria;

                    Modelo.Result result = Negocio.AlumnoMateria.AgregarAlumnoMateria(alumnoMateria);
                }

                result1.Correct = true;
                ViewBag.Massage = "Se actualizo correctamente el alumno. " + result1.Message;
            }
            else
            {
                result1.Correct = false;
            }
            return PartialView("Modal");
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
                ViewBag.Massage = "Se ha eliminado exitosamente el registro";
            }
            else
            {
                ViewBag.Massage = "ocurrió un error al eliminar el registro " + result.ErrorMessage;

            }
            return PartialView("Modal");
        }

    }
}
