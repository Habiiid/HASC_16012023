using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Servicio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        //GetAll
        [EnableCors("API")]
        [HttpGet("MostrarTodasLasMaterias")]
        public IActionResult GetAll()
        {
            Modelo.Result result = Negocio.Materia.MostrarTodasLasMaterias();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        // GetById
        [EnableCors("API")]
        [HttpGet("MostrarUnaMateria/{idmateria}")]
        public IActionResult Get(int idmateria)
        {
            Modelo.Result result = Negocio.Materia.MostrarSoloUnaMateria(idmateria);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        // Agregar
        [EnableCors("API")]
        [HttpPost("AgregarMateria")]
        public IActionResult Post([FromBody] Modelo.Materia materia)
        {
            Modelo.Result result = Negocio.Materia.AgregarMateria(materia);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // UPDATE
        [EnableCors("API")]
        [HttpPost("ActualizarMateria")]
        public IActionResult Put([FromBody] Modelo.Materia materia)
        {
            
            var result =  Negocio.Materia.ActualizarMateria(materia);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // DELETE 
        [EnableCors("API")]
        [HttpDelete("Eliminar/{IdMateria}")]
        public IActionResult Delete(int idmateria)
        {
            if (idmateria > 0)
            {
                Modelo.Result result = Negocio.Materia.EliminarMateria(idmateria);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }

    }
}


