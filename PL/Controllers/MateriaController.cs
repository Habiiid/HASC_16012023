using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            Modelo.Materia materia = new Modelo.Materia();
            Modelo.Result result = new Modelo.Result();

            result = Negocio.Materia.MostrarTodasLasMaterias(materia);

            if (result.Correct)
            {
                materia.Materias = result.Objects;
                return View(materia);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al mostrar las materias.";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Formulario(int? IdMateria)
        {

            Modelo.Materia materia = new Modelo.Materia();

            if (IdMateria == null)
            {
                return View(materia);
            }
            else
            {
                //GetById

                var result = Negocio.Materia.MostrarSoloUnaMateria(IdMateria.Value);

                if (result.Correct)
                {
                    materia = (Modelo.Materia)result.Object;

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error";
                }
                return Json(materia);
            }

        }

        [HttpPost]
        public ActionResult Formulario(Modelo.Materia materia)
        {

            Modelo.Result result = new Modelo.Result();

          
            if (materia.IdMateria == 0)
            {
                //ADD
                result = Negocio.Materia.AgregarMateria(materia);
                if (result.Correct)
                {
                    ViewBag.Message = result.Message;
                    ViewBag.Massage = "Se Agrego correctamente la materia. " + result.Message;
                }
                else
                {
                    ViewBag.Message = "Error" + result.Message;
                }
            }
            else
            {
                //Update
                result = Negocio.Materia.ActualizarMateria(materia);

                if (result.Correct)
                {
                    ViewBag.Massage = "Se actualizo correctamente la materia. " + result.Message;
                }
                else
                {
                    ViewBag.Massage = "Error: " + result.Message;
                }
            }
            //return PartialView("Modal");

            return View(materia);
        }


        public ActionResult Delete(int idMateria)
        {
            Modelo.Result result = Negocio.Materia.EliminarMateria(idMateria);

            if (idMateria != null)
            {
                if (result.Correct)
                {
                    ViewBag.Massage = result.Message;
                }
                else
                {
                    ViewBag.Massage = "Error: " + result.Message;
                }
            }
            else
            {
                return Redirect("/Materia/Index");
            }
            return View();
        }

    }
}
