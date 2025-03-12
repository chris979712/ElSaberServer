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
    public class LibroOperaciones
    {
        public int ObtenerIdLibroPorCodigoISBN(string isbn) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int idLibro = Constantes.ErrorEnLaOperacion;
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var libro=contextoBaseDeDatos.Libro.FirstOrDefault(entidad=>entidad.isbn==isbn);
                    if (libro != null) 
                    {
                        idLibro = libro.IdLibro;
                    }                
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
            return idLibro;
        }

        public int ValidarExistenciaLibrosEnLaBaseDeDatos() 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoConsulta = Constantes.ErrorEnLaOperacion;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var existenciaLibros = contextoBaseDeDatos.Libro.Any();
                    resultadoConsulta = existenciaLibros ? Constantes.OperacionExitosa : Constantes.SinResultadosEncontrados;
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
            return resultadoConsulta;
        }
    }
}
