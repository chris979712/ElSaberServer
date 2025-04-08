using ElSaberDataAccess.Utilities;
using ElSaberDataAccess.Utilidades;
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
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
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

        public int VerificarCorreoExistente(string correo)
        {
            int resultadoVerificacion = Constantes.ErrorEnLaOperacion;
            LoggerManager logger = new LoggerManager(this.GetType());
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var cuentaExistente = contextoBaseDeDatos.Acceso.Where(cuenta => cuenta.correo == correo).FirstOrDefault();
                    if (cuentaExistente != null)
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

        public int ModificarContrasenia(string correo, string contrasenia)
        {
            int resultadoModificacion = Constantes.ErrorEnLaOperacion;
            LoggerManager logger = new LoggerManager(this.GetType());
            try
            {
                using(var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var cuentaExistente = contextoBaseDeDatos.Acceso.Where(cuenta=>cuenta.correo.Equals(correo)).FirstOrDefault();
                    if(cuentaExistente!= null)
                    {
                        cuentaExistente.contrasenia = contrasenia;
                        contextoBaseDeDatos.SaveChanges();
                        resultadoModificacion = Constantes.OperacionExitosa;
                    }
                    else
                    {
                        resultadoModificacion = Constantes.SinResultadosEncontrados;
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
            return resultadoModificacion;
        }

        public DatosUsuario IniciarSesion(string correo, string contrasenia)
        {
            DatosUsuario usuario = new DatosUsuario()
            {
                IdAcceso = Constantes.ErrorEnLaOperacion
            };

            LoggerManager logger = new LoggerManager(this.GetType());
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var usuarioObtenido = contextoBaseDeDatos.Acceso.Where(acceso => acceso.correo == correo && acceso.contrasenia == contrasenia).Join(
                        contextoBaseDeDatos.Usuario, acceso => acceso.FK_IdUsuario, u => u.IdUsuario, (acceso, u) => new DatosUsuario
                        {
                            IdAcceso = acceso.IdAcceso,
                            Correo = acceso.correo,
                            TipoDeUsuario = acceso.tipoDeUsuario,
                            FK_IdUsuario = u.IdUsuario,
                            Nombre = u.nombre,
                            PrimerApellido = u.primerApellido,
                            SegundoApellido = u.segundoApellido,
                            Puesto = u.puesto,
                            Estado = u.estado
                        }).FirstOrDefault();
                    
                    if (usuarioObtenido != null)
                    {
                        if (usuarioObtenido.Estado == Enumeradores.EnumeradorEstadoUsuario.Activo.ToString())
                        {
                            usuario = usuarioObtenido;
                        }
                        else
                        {
                            usuario.IdAcceso = Constantes.SinResultadosEncontrados;
                        }
                    }
                    else
                    {
                        usuario.IdAcceso = Constantes.SinResultadosEncontrados;
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
            return usuario;
        }

        public List<Acceso> ObtenerUsuarios()
        {
            List<Acceso> accesosObtenidos = new List<Acceso>();
            Acceso accesoError = new Acceso()
            {
                IdAcceso = Constantes.ErrorEnLaOperacion
            };

            LoggerManager logger = new LoggerManager(this.GetType());
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    accesosObtenidos = contextoBaseDeDatos.Acceso
                        .Include("Usuario")
                        .Include("Usuario.Direccion")
                        .Where(a => a.Usuario.estado == Enumeradores.EnumeradorEstadoUsuario.Activo.ToString())
                        .ToList();
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                accesosObtenidos.Add(accesoError);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                accesosObtenidos.Add(accesoError);
            }
            return accesosObtenidos;
        }
    }
}
