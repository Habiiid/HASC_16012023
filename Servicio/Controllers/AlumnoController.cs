using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Servicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        [HttpGet("GetByApellido/{apellidoPaterno}")]
        public IActionResult Login(string apellidoPaterno)
        {
            
            Modelo.Result result = Negocio.Alumno.GetByApellido(apellidoPaterno);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
