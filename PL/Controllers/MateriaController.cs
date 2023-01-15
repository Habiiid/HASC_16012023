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

            result = Negocio.Materia.MostrarTodasLasMaterias();

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
    }
}
