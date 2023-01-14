using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public AlumnoController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

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
        public ActionResult Formulario(int? IdAlumno)
        {

            Modelo.Alumno alumno = new Modelo.Alumno(); 

            if (IdAlumno == null) 
            {
                return View(alumno);   
            }
            else
            {
                //GetById
                
                var result = Negocio.Alumno.MostrarUnAlumno(IdAlumno.Value);

                if (result.Correct)
                {
                    alumno = (Modelo.Alumno)result.Object;
                    
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error";
                }
                return Json(alumno);
            }

        }

        [HttpPost]
        public ActionResult Formulario(Modelo.Alumno alumno)
        {
           
            Modelo.Result result = new Modelo.Result();

            IFormFile image = Request.Form.Files["IFImagen"];
            if (image != null)
            {
                byte[] ImagenBytes = ConvertToBytes(image);
                alumno.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            if (alumno.IdAlumno == 0)
                {
                    //ADD
                    result = Negocio.Alumno.AgregarAlumno(alumno);
                    if (result.Correct)
                    {
                        ViewBag.Message = result.Message;
                        ViewBag.Massage = "Se Agrego correctamente el alumno. " + result.Message;
                }
                    else
                    {
                        ViewBag.Message = "Error" + result.Message;
                    }
                }
                else
                {
                    //Update
                    result = Negocio.Alumno.ActualizarAlumno(alumno);

                    if (result.Correct)
                    {
                        ViewBag.Massage = "Se actualizo correctamente el alumno. " + result.Message;
                    }
                    else
                    {
                        ViewBag.Massage = "Error: " + result.Message;
                    }
                }
                //return PartialView("Modal");
            
            return View(alumno);
        }


        public ActionResult Delete(int idAlumno)
        {
            Modelo.Result result = Negocio.Alumno.EliminarAlumno(idAlumno);

            if (idAlumno != null)
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
                return Redirect("/Alumno/Index");
            }
            return View();
        }


        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        //login
        [HttpGet] 
        public ActionResult Login(){

            return View();
        }


        [HttpPost]
        public ActionResult Login(string Nombre, string ApellidoPaterno)
        {
           

            Modelo.Result result = new Modelo.Result();

            try
            {
                string urlAPI = _configuration["UrlAPI"];
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlAPI);

                    var responseTask = client.GetAsync("Alumno/GetByApellido/" + ApellidoPaterno);

                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadFromJsonAsync<Modelo.Result>();
                        readTask.Wait();

                        Modelo.Alumno alumnoItem = new Modelo.Alumno();

                        alumnoItem = Newtonsoft.Json.JsonConvert.DeserializeObject<Modelo.Alumno>(readTask.Result.Object.ToString());
                        result.Object = alumnoItem;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }

            if (result.Correct)
            {
                Modelo.Alumno alumno = (Modelo.Alumno)result.Object;

                if (alumno.ApellidoPaterno == ApellidoPaterno)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "El  Nombre o Apellido es incorrecto..";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "El Nombre o Apellido es incorrecto..";
                return PartialView("ModalLogin");
            }



        }

    }
}


