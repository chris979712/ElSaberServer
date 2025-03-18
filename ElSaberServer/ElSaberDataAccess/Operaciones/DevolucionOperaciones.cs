using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberDataAccess.Operaciones
{
    public class DevolucionOperaciones
    {
        public int RegistrarDevolucionEnLaBaseDeDatos(Devolucion devolucion) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.ErrorEnLaOperacion;
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()) 
                {

                    var prestamo = contextoBaseDeDatos.Prestamo.FirstOrDefault(entidad => entidad.IdPrestamo == devolucion.FK_IdPrestamo);
                    if (prestamo != null) 
                    {
                        prestamo.estado = Enumeradores.EnumeradoEstadoPrestamo.Devuelto.ToString();
                    }                    
                    var nuevaDevolucion = new Devolucion() 
                    {
                        FK_IdPrestamo=devolucion.FK_IdPrestamo,
                        fechaDevolucion=devolucion.fechaDevolucion,
                        nota=devolucion.nota,
                        estadoLibro=devolucion.estadoLibro,
                    };
                    contextoBaseDeDatos.Devolucion.Add(nuevaDevolucion);
                    contextoBaseDeDatos.SaveChanges();
                    resultadoInsercion = Constantes.OperacionExitosa;
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
            }
            return resultadoInsercion;
        }
    }
}
