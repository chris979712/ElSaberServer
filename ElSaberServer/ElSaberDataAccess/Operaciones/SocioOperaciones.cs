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
    public class SocioOperaciones
    {

        public int RegistrarNuevoSocio(Socio socio, Direccion direccion)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.OperacionExitosa;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
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
                                var socioNuevo = new Socio
                                {
                                    nombre = socio.nombre,
                                    primerApellido = socio.primerApellido,
                                    segundoApellido = socio.segundoApellido,
                                    telefono = socio.telefono,
                                    fechaInscripcion = socio.fechaInscripcion,
                                    estado = Enumeradores.EnumeradorEstadoUsuario.Activo.ToString(),
                                    FK_idDireccion = direccionIngresada.IdDireccion
                                };
                                contextoBaseDeDatos.Direccion.Add(direccionNueva);
                                contextoBaseDeDatos.SaveChanges();
                                contextoTransaccionBaseDeDatos.Commit();
                                resultadoInsercion = Constantes.OperacionExitosa;
                            }
                        }
                        catch (SqlException sqlException)
                        {
                            logger.LogWarn(sqlException);
                            contextoTransaccionBaseDeDatos.Rollback();
                        }
                        catch (DbUpdateException dbUpdateException)
                        {
                            logger.LogError(dbUpdateException);
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

        public int VerificarSocioRegistradoEnLaBaseDeDatos(string telefono)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoBusqueda = Constantes.SinResultadosEncontrados;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var usuarioEncontrado = contextoBaseDeDatos.Socio.Where(usuario => usuario.telefono.Equals(telefono)).FirstOrDefault();
                    if (usuarioEncontrado == null)
                    {
                        resultadoBusqueda = Constantes.SinResultadosEncontrados;
                    }
                    else
                    {
                        resultadoBusqueda = Constantes.ResultadosCoincidentes;
                    }
                }
            }
            catch(SqlException sqlException)
            {
                logger.LogError(sqlException);
            }
            catch(EntityException entityException)
            {
                logger.LogFatal(entityException);   
            }
            return resultadoBusqueda;
        }

        public List<Socio> ObtenerSociosPorNombre(string nombre)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Socio> sociosObtenidos = new List<Socio>();
            Socio errorEnLaConecion = new Socio()
            {
                numeroDeSocio = Constantes.ErrorEnLaOperacion
            };
            try
            {
                using(var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var nombrePorPartes = nombre.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    var consulta = contextoBaseDeDatos.Socio.AsQueryable();
                    IQueryable<Socio> resultadoConsulta = consulta;
                    switch (nombrePorPartes.Length)
                    {
                        case 1:
                            resultadoConsulta = consulta.Where(socio => socio.nombre.Contains(nombrePorPartes[0]));
                            break;
                        case 2:
                            resultadoConsulta = consulta.Where(socio => socio.nombre.Contains(nombrePorPartes[0]) &&
                                socio.primerApellido.Contains(nombrePorPartes[1]));
                            break;
                        case 3:
                            resultadoConsulta = consulta.Where(socio => socio.nombre.Contains(nombrePorPartes[0]) &&
                                socio.primerApellido.Contains(nombrePorPartes[1]) && socio.segundoApellido.Contains(nombrePorPartes[2]));
                            break;
                    }
                    if (resultadoConsulta.Count() > 0)
                    {
                        sociosObtenidos = resultadoConsulta.ToList();
                    }
                    else {
                        sociosObtenidos.Add(new Socio()
                        {
                           numeroDeSocio = Constantes.SinResultadosEncontrados
                        });
                    }
                }
            }
            catch(SqlException sqlException)
            {
                logger.LogError(sqlException);
                sociosObtenidos.Add(errorEnLaConecion);
            }
            catch(EntityException entityException)
            {
                logger.LogFatal(entityException);
                sociosObtenidos.Add(errorEnLaConecion);
            }
            return sociosObtenidos;
        }
        
        public Socio ObtenerSocioPorNumeroDeSocio(int numeroDeSocio)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            Socio socioObtenido = new Socio();
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var socioObtenidoConsulta = contextoBaseDeDatos.Socio.Where(socio => socio.numeroDeSocio == numeroDeSocio).FirstOrDefault();
                    if(socioObtenidoConsulta!= null)
                    {
                        var direccionDeSocioConsulta = contextoBaseDeDatos.Direccion.Where(direccion => direccion.IdDireccion == socioObtenidoConsulta.FK_idDireccion).FirstOrDefault();
                        socioObtenido = socioObtenidoConsulta;
                        socioObtenido.Direccion = direccionDeSocioConsulta;
                    }
                    else
                    {
                        socioObtenido.numeroDeSocio = Constantes.SinResultadosEncontrados;
                    }
                    
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                socioObtenido.numeroDeSocio = Constantes.ErrorEnLaOperacion;
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                socioObtenido.numeroDeSocio = Constantes.ErrorEnLaOperacion;
            }
            return socioObtenido;
        }
    }
}
