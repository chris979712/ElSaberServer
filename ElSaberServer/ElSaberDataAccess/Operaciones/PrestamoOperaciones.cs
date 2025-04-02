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

                    var libro = contextoBaseDeDatos.Libro.FirstOrDefault(entidad=>entidad.IdLibro==prestamo.FK_IdLibro);
                    if (libro != null) 
                    {
                        if (libro.cantidadEjemplares > Constantes.ValorPorDefecto) 
                        {
                            libro.cantidadEjemplares -= 1;
                            libro.cantidadEjemplaresPrestados += 1;
                        }
                    }
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

        public List<Prestamo> RecuperarPrestamosActivosYVencidosPorNumeroSocio(int numeroSocio) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Prestamo> prestamosObtenidos = new List<Prestamo>();
            Prestamo prestamo = new Prestamo()
            {
                IdPrestamo = Constantes.ErrorEnLaOperacion,
            };
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()) 
                {
                    prestamosObtenidos = contextoBaseDeDatos.Prestamo
                    .Where(entidad => entidad.FK_IdSocio == numeroSocio &&
                    (entidad.estado == Enumeradores.EnumeradoEstadoPrestamo.Activo.ToString() ||
                    entidad.estado == Enumeradores.EnumeradoEstadoPrestamo.Vencido.ToString()))
                    .ToList();

                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                prestamosObtenidos.Add(prestamo);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                prestamosObtenidos.Add(prestamo);
            }
            return prestamosObtenidos;
        }

        public int ValidarExistenciaPrestamosVencidosPorNumeroSocio(int numeroSocio) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoValidacion = Constantes.ErrorEnLaOperacion;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    bool tienePrestamosVencidos = contextoBaseDeDatos.Prestamo
                    .Any(entidad => entidad.FK_IdSocio == numeroSocio && entidad.estado == Enumeradores.EnumeradoEstadoPrestamo.Vencido.ToString());
                    resultadoValidacion = tienePrestamosVencidos ? Constantes.OperacionExitosa : Constantes.ValorPorDefecto;
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
            return resultadoValidacion;
        }

        public List<SocioPrestamoPendiente> ObtenerPrestamosPendientes()
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<SocioPrestamoPendiente> sociosConPrestamosPendientes = new List<SocioPrestamoPendiente>();
            SocioPrestamoPendiente socioExcepcion = new SocioPrestamoPendiente()
            {
                idPrestamo = -1
            };
            try
            {
                using(var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var listaPrestamosPendientes = contextoBaseDeDatos.Prestamo.Where(prestamodb => prestamodb.estado == Enumeradores.EnumeradoEstadoPrestamo.Activo.ToString()).ToList();
                    if(listaPrestamosPendientes.Count > 0)
                    {
                        foreach(var prestamo in listaPrestamosPendientes)
                        {
                            SocioPrestamoPendiente socioPrestamoPendiente = new SocioPrestamoPendiente()
                            {
                                idPrestamo = prestamo.IdPrestamo,
                                isbnLibro = prestamo.Libro.isbn,
                                telefonoSocio = prestamo.Socio.telefono,
                                numeroSocio = prestamo.Socio.numeroDeSocio,
                                tituloLibro = prestamo.Libro.titulo,
                                fechaDevolucionEsperada = prestamo.fechaDevolucionEsperada.ToString(),
                                fechaPrestamo = prestamo.fechaPrestamo.ToString(),
                                nombreSocio = prestamo.Socio.nombre+" "+prestamo.Socio.primerApellido+" "+prestamo.Socio.segundoApellido
                            };
                            sociosConPrestamosPendientes.Add(socioPrestamoPendiente);
                        }
                    }
                    else
                    {
                        SocioPrestamoPendiente socioSinPrestamoPendiente = new SocioPrestamoPendiente()
                        {
                            idPrestamo = 0
                        };
                        sociosConPrestamosPendientes.Add(socioSinPrestamoPendiente);
                    }
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                sociosConPrestamosPendientes.Add(socioExcepcion);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                sociosConPrestamosPendientes.Add(socioExcepcion);
            }
            return sociosConPrestamosPendientes;
        }
    }
}
