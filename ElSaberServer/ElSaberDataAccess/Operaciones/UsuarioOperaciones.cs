using ElSaberDataAccess;
using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using log4net.Repository.Hierarchy;
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
    public class UsuarioOperaciones
    {

        public int RegistrarUsuarioEnLaBaseDeDatos(Usuario usuario, Acceso acceso, Direccion direccion)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.ErrorEnLaOperacion;
            DireccionOperaciones operacionDireccion = new DireccionOperaciones();
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()){
                    using (var contextoTransaccionBaseDeDatos = contextoBaseDeDatos.Database.BeginTransaction())
                    {
                        try
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
                            var direccionIngresada = contextoBaseDeDatos.Direccion.Where(direccionInsertada => direccionInsertada
                            .ciudad == direccion.ciudad && direccionInsertada.calle == direccionInsertada.calle &&
                            direccionInsertada.numero == direccion.numero).FirstOrDefault();
                            if (direccionIngresada != null)
                            {
                                var nuevoUsuarioAInsertar = new Usuario
                                {
                                    nombre = usuario.nombre,
                                    primerApellido = usuario.primerApellido,
                                    segundoApellido = usuario.segundoApellido,
                                    telefono = usuario.telefono,
                                    puesto = usuario.puesto,
                                    estado = Enumeradores.EnumeradorEstadoUsuario.Activo.ToString(),
                                    FK_idDireccion = direccionIngresada.IdDireccion
                                };
                                contextoBaseDeDatos.Usuario.Add(nuevoUsuarioAInsertar);
                                contextoBaseDeDatos.SaveChanges();
                                int idUsuarioIngresado = nuevoUsuarioAInsertar.IdUsuario;
                                var nuevoAccesoAInsertar = new Acceso
                                {
                                    correo = acceso.correo,
                                    contrasenia = acceso.contrasenia,
                                    tipoDeUsuario = acceso.tipoDeUsuario,
                                    FK_IdUsuario = idUsuarioIngresado
                                };
                                contextoBaseDeDatos.Acceso.Add(nuevoAccesoAInsertar);
                                contextoBaseDeDatos.SaveChanges();
                                contextoTransaccionBaseDeDatos.Commit();
                                resultadoInsercion = Constantes.OperacionExitosa;
                            }
                        }
                        catch (DbUpdateException dbUpdateException)
                        {
                            logger.LogWarn(dbUpdateException);
                            contextoTransaccionBaseDeDatos.Rollback();
                        }
                        catch (SqlException sqlException)
                        {
                            logger.LogError(sqlException);
                            contextoTransaccionBaseDeDatos.Rollback();
                        }
                    }
                }
            }
            catch(EntityException entityException)
            {
                logger.LogFatal(entityException);
            }
            return resultadoInsercion;
        }

        public int VerificarExistenciaDeUsuario(string telefono, string correo)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoVerificacion = Constantes.ErrorEnLaOperacion;
            try
            {
                using(var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var usuarioExistente = contextoBaseDeDatos.Usuario.Where(usuario => usuario.telefono == telefono).FirstOrDefault();
                    var cuentaExistente = contextoBaseDeDatos.Acceso.Where(acceso => acceso.correo == correo).FirstOrDefault();
                    if (usuarioExistente != null || cuentaExistente != null)
                    {
                        resultadoVerificacion = Constantes.ResultadosCoincidentes;
                    }
                    else
                    {
                        resultadoVerificacion = Constantes.SinResultadosEncontrados;
                    }
                }
            }
            catch (SqlException sqlException) {
                logger.LogError(sqlException);
            }
            catch(EntityException entityException)
            {
                logger.LogFatal(entityException);
            }
            return resultadoVerificacion;
        }

        public int DesactivarUsuarioPorId(int idUsuario) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoDesactivacion = Constantes.ErrorEnLaOperacion;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var usuario = contextoBaseDeDatos.Usuario.FirstOrDefault(entidad => entidad.IdUsuario == idUsuario);
                    if (usuario != null)
                    {
                        usuario.estado = Enumeradores.EnumeradorEstadoUsuario.Desactivado.ToString();
                        resultadoDesactivacion = contextoBaseDeDatos.SaveChanges();
                    }
                    else 
                    {
                        resultadoDesactivacion = Constantes.ValorPorDefecto;
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
            return resultadoDesactivacion;
        }

        public int EditarUsuarioPorIdAcceso(int idAcceso, Usuario usuarioEditado,string correoAcceso) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoModificacion = Constantes.ErrorEnLaOperacion;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    using (var transaccionBaseDeDatos = contextoBaseDeDatos.Database.BeginTransaction())
                    {
                        try
                        {
                            var acceso = contextoBaseDeDatos.Acceso.FirstOrDefault(entidad => entidad.IdAcceso == idAcceso);
                            if (acceso != null)
                            {
                                var usuario = contextoBaseDeDatos.Usuario.FirstOrDefault(usuarioLambda => usuarioLambda.IdUsuario == acceso.FK_IdUsuario);
                                var direccion = contextoBaseDeDatos.Direccion.FirstOrDefault(direccionLambda => direccionLambda.IdDireccion == usuario.FK_idDireccion);                                
                                direccion.ciudad = usuarioEditado.Direccion.ciudad;
                                direccion.codigoPostal = usuarioEditado.Direccion.codigoPostal;
                                direccion.calle = usuarioEditado.Direccion.calle;
                                direccion.numero = usuarioEditado.Direccion.numero;
                                usuario.nombre= usuarioEditado.nombre;
                                usuario.primerApellido = usuarioEditado.primerApellido;
                                usuario.segundoApellido = usuarioEditado.segundoApellido;
                                usuario.telefono=usuarioEditado.telefono;
                                usuario.puesto= usuarioEditado.puesto;
                                acceso.correo = correoAcceso;
                                contextoBaseDeDatos.SaveChanges();
                                transaccionBaseDeDatos.Commit();
                                resultadoModificacion = Constantes.OperacionExitosa;
                            }
                            else
                            {
                                resultadoModificacion = Constantes.SinResultadosEncontrados;
                            }
                        }
                        catch (DbUpdateException dpUpdateException)
                        {
                            logger.LogError(dpUpdateException);
                            transaccionBaseDeDatos.Rollback();
                        }
                        catch (EntityException entityException)
                        {
                            logger.LogFatal(entityException);
                            transaccionBaseDeDatos.Rollback();
                        }
                    }
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
            }
            return resultadoModificacion;
        }
    }
}
