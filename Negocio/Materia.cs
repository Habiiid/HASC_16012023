using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Materia
    {
        public static Modelo.Result MostrarTodasLasMaterias()
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    var materias = context.Materia.FromSqlRaw("MostrarTodasLasMaterias").ToList();
                    result.Objects = new List<object>();
                    if (materias != null)
                    {
                        foreach (var row in materias)
                        {
                            Modelo.Materia materia = new Modelo.Materia();

                            materia.IdMateria = row.IdMateria;
                            materia.Nombre = row.Nombre;
                            materia.Costo = row.Costo.Value;
                            materia.Descripcion = row.Descripcion;


                            result.Objects.Add(materia);

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

        public static Modelo.Result MostrarSoloUnaMateria(int idMateria)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {

                    var materias = context.Materia.FromSqlRaw($"MostrarSoloUnaMateria  {idMateria}").AsEnumerable().SingleOrDefault();

                    if (materias != null)
                    {

                        Modelo.Materia materia = new Modelo.Materia();

                        materia.IdMateria = materias.IdMateria;
                        materia.Nombre = materias.Nombre;
                        materia.Costo = materias.Costo.Value;
                        materia.Descripcion = materias.Descripcion;

                        result.Object = materia;
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

        public static Modelo.Result AgregarMateria(Modelo.Materia materia)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {

                    var materias = context.Database.ExecuteSqlRaw($"AgregarMateria '{materia.Nombre}', {materia.Costo}, '{materia.Descripcion}' ");

                    if (materias > 0)
                    {
                        result.Correct = true;
                        result.Message = "Se agrego la materia correctamente.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido agregar la materia";

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

        public static Modelo.Result ActualizarMateria(Modelo.Materia materia)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"ActualizarMateria {materia.IdMateria}, '{materia.Nombre}', {materia.Costo}, '{materia.Descripcion}'");

                    if (query > 0)
                    {
                        result.Message = "Se actualizo la materia correctamente.";
                    }

                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = "No se pudo actualizar la materia" + result.Excepcion;

                throw;
            }
            return result;

        }

        public static Modelo.Result EliminarMateria(int idMateria)
        {
            Modelo.Result result = new Modelo.Result();
            try
            {
                using (Datos.ControlEscolarContext context = new Datos.ControlEscolarContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"EliminarMateria {idMateria}");

                    if (query > 0)
                    {
                        result.Message = "Se elimino la materia correctamente.";
                    }

                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Excepcion = ex;
                result.ErrorMessage = "No se pudo eliminar la materia" + result.Excepcion;

                throw;
            }
            return result;
        }
    }
}
