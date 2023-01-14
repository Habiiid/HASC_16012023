using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Alumno
    {

        public static Modelo.Result MostrarTodosLosAlumnos(Modelo.Alumno alumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    var alumnos = context.Alumnos.FromSqlRaw("MostrarTodosLosAlumnos").ToList();
                    result.Objects = new List<object>();
                    if (alumnos != null)
                    {
                        foreach (var row in alumnos)
                        {
                            Modelo.Alumno alumno1 = new Modelo.Alumno();

                            alumno1.IdAlumno = row.IdAlumno;
                            alumno1.Nombre = row.Nombre;
                            alumno1.ApellidoPaterno = row.ApellidoPaterno;
                            alumno1.ApellidoMaterno = row.ApellidoMaterno;
                            alumno1.Imagen = row.Imagen;


                            result.Objects.Add(alumno1);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static Modelo.Result MostrarUnAlumno(int idAlumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {

                    var query = context.Alumnos.FromSqlRaw($"MostrarSoloUnAlumno  {idAlumno}").AsEnumerable().SingleOrDefault();

                    if (query != null)
                    {

                        Modelo.Alumno alumno = new Modelo.Alumno();

                        alumno.IdAlumno = query.IdAlumno;
                        alumno.Nombre = query.Nombre;
                        alumno.ApellidoPaterno = query.ApellidoPaterno;
                        alumno.ApellidoMaterno = query.ApellidoMaterno;
                        alumno.Imagen = query.Imagen;

                        result.Object = alumno;
                    }

                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = "No se pudo mostar al alumno que seleccionaste " + result.Excepcion;

                throw;
            }

            return result;
        }
       
        public static Modelo.Result AgregarAlumno(Modelo.Alumno alumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    
                    var alumnos = context.Database.ExecuteSqlRaw($"AgregarAlumno '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.Imagen}' ");

                    if (alumnos > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se agrego al alumno correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido agregar al alumno";

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }    

        public static Modelo.Result ActualizarAlumno(Modelo.Alumno alumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ActualizarAlumno {alumno.IdAlumno}, '{alumno.Nombre}', '{alumno.ApellidoPaterno}', '{alumno.ApellidoMaterno}', '{alumno.Imagen}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo al alumno correctamente.";
                    }

                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = "No se pudo actualizar al alumno." + result.Excepcion;

                throw;
            }
            return result;

        }

        public static Modelo.Result EliminarAlumno(int IdAlumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EliminarAlumno {IdAlumno}");

                    if (query > 0)
                    {
                        result.Message = "Se elimino al alumno correctamente.";
                    }

                }
                result.Correct = true;
            } 
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = "No se pudo eliminar al alumno." + result.Excepcion;

                throw;
            }
            return result;
        }

        public static Modelo.Result GetByApellido(string ApellidoPaterno)
        {
            Modelo.Result result = new Modelo.Result();

            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    
                    var objAlumno = context.Alumnos?.FromSqlRaw($"AlumnoGetByApellido '{ApellidoPaterno}'").AsEnumerable().FirstOrDefault();
                    if (objAlumno != null)
                    {
                        Modelo.Alumno alumno = new Modelo.Alumno();
                        alumno.IdAlumno = objAlumno.IdAlumno;
                        alumno.Nombre = objAlumno.Nombre;
                        alumno.ApellidoPaterno = objAlumno.ApellidoPaterno;
                        alumno.ApellidoMaterno = objAlumno.ApellidoMaterno;
                        alumno.Imagen = objAlumno.Imagen;
                       

                        result.Object = alumno;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.Message = "Error: " + result.Excepcion;
            }

            return result;
        }

    }
}