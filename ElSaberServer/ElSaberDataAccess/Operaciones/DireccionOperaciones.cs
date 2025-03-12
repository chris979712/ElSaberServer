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
    public class DireccionOperaciones
    {
        public int AgregarNuevaDireccion(Direccion direccion)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.ErrorEnLaOperacion;
            try
            { 
                using(var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var direccionNueva = new Direccion
                    {
                        calle = direccion.calle,
                        numero = direccion.numero,
                        codigoPostal = direccion.codigoPostal,
                        ciudad = direccion.ciudad,
                    };
                    contextoBaseDeDatos.Direccion.Add(direccionNueva);
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
