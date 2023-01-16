using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Negocio
{
    public class AlumnoMateria
    {

        public static Modelo.Result GetAllMateriasAsginadas(int IdAlumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
               
                    var alumnos = context.AlumnoMateria.FromSqlRaw($"AlumnoMateriaMostrarPorAlumno {IdAlumno}").ToList();

                    result.Objects = new List<object>();

                    if (alumnos != null)
                    {
                        foreach (var row in alumnos)
                        {
                            Modelo.AlumnoMateria alumnoMateria = new Modelo.AlumnoMateria();

                            alumnoMateria.IdAlumnoMateria = row.IdAlumnoMateria.Value;

                            alumnoMateria.Alumno = new Modelo.Alumno();
                            alumnoMateria.Alumno.IdAlumno = row.IdAlumno.Value;
                            alumnoMateria.Alumno.Nombre = row.NombreAlumno;
                            alumnoMateria.Alumno.ApellidoPaterno = row.ApellidoPaterno;
                            alumnoMateria.Alumno.ApellidoMaterno = row.ApellidoMaterno;

                            alumnoMateria.Materia = new Modelo.Materia();
                            alumnoMateria.Materia.IdMateria = row.IdMateria.Value;
                            alumnoMateria.Materia.Nombre = row.NombreMateria;
                            alumnoMateria.Materia.Costo = decimal.Parse(row.Costo.ToString());

                            result.Objects.Add(alumnoMateria);
                        }
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = "Ocurrio un error al mostrar las materias" + result.Excepcion;
            }
            return result;
        }

        public static Modelo.Result MateriasNoAsignadas(int IdAlumno)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                
                    var alumnos = context.Materia.FromSqlRaw($"MateriasNoAsignadasAlAlumno {IdAlumno}").ToList();

                    result.Objects = new List<object>();

                    if (alumnos != null)
                    {

                        foreach (var row in alumnos)
                        {
                            Modelo.AlumnoMateria alumnomateria = new Modelo.AlumnoMateria();

                            alumnomateria.Materia = new Modelo.Materia();
                            alumnomateria.Materia.IdMateria = row.IdMateria;
                            alumnomateria.Materia.Nombre = row.MateriaNombre;
                            alumnomateria.Materia.Costo = row.Costo;

                            result.Objects.Add(alumnomateria);
                       
                        }
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo realizar la consulta";
                    }
                    
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Excepcion = ex;
            }
            return result;
        }

        public static Modelo.Result AgregarAlumnoMateria(Modelo.AlumnoMateria alumnoMateria)
        {
            Modelo.Result result = new Modelo.Result();

            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    // var query = context.AlumnoMateriaAdd(alumnoMateria.Alumno.IdAlumno, alumnoMateria.Materia.IdMateria);
                    var alumnos = context.Database.ExecuteSqlRaw($"AgregarAlumnoMateria {alumnoMateria.IdAlumno} , {alumnoMateria.IdMateria}");
                    {
                        if (alumnos != null)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se ha podido insertar la(s) nueva(s) materia(s)";
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static Modelo.Result EliminarAlumnoMateria(Modelo.AlumnoMateria alumnoMateria)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    // var query = context.AlumnoMateriaDelete(alumnomateria.IdAlumnoMateria);
                    var alumnos = context.Database.ExecuteSqlRaw($"EliminarAlumnoMateria {alumnoMateria.IdAlumnoMateria}");
                    result.Object = alumnoMateria;

                    if (alumnos != null)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al eliminar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
