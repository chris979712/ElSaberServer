using ElSaberDataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberDataAccess.Operaciones
{
    public class AccesoOperaciones
    {
        public int VerificarCredenciales(string correo, string contrasenia)
        {
            int resultadoVerificacion = Constantes.ErrorEnLaOperacion;
            LoggerManager logger = new LoggerManager(this.GetType());
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDataModel())
                {
                    var cuentaExistente = contextoBaseDeDatos.Acceso.Where(cuenta => cuenta.correo == correo && contrasenia == cuenta.contrasenia).FirstOrDefault();
                    if(cuentaExistente != null)
                    {
                        resultadoVerificacion = Constantes.ResultadosCoincidentes;
                    }
                    else
                    {
                        resultadoVerificacion = Constantes.SinResultadosEncontrados;
                    }
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
            }
            return resultadoVerificacion;
        }
    }
}
