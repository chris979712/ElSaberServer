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
    public class PrestamoOperaciones
    {
        public int RegistrarPrestamoEnLaBaseDeDatos(Prestamo prestamo) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.ErrorEnLaOperacion;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var nuevoPrestamo = new Prestamo 
                    {
                        fechaPrestamo=prestamo.fechaPrestamo,
                        estado=Enumeradores.EnumeradoEstadoPrestamo.Activo.ToString(),
                        fechaDevolucionEsperada=prestamo.fechaDevolucionEsperada,
                        nota=prestamo.nota,
                        FK_IdLibro=prestamo.FK_IdLibro,
                        FK_IdUsuario=prestamo.FK_IdUsuario,
                        FK_IdSocio=prestamo.FK_IdSocio,
                    };
                    contextoBaseDeDatos.Prestamo.Add(nuevoPrestamo);
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
