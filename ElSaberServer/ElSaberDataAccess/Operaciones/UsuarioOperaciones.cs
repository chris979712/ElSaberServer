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
                    int resultadoInsercionDireccion = operacionDireccion.AgregarNuevaDireccion(direccion);
                    if (resultadoInsercion == Constantes.OperacionExitosa)
                    {
                        using (var contextoTransaccionBaseDeDatos = contextoBaseDeDatos.Database.BeginTransaction())
                        {
                            try
                            {
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
                    if (usuarioExistente != null)
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
    }
}
