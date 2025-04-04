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
    public class MultaOperaciones
    {
        public List<Multa> RecuperarMultasPendientesPorNumeroSocio(int numeroSocio) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Multa> multasObtenidas= new List<Multa>();
            Multa multaError = new Multa()
            {
                IdMulta = Constantes.ErrorEnLaOperacion,
            };
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {                    
                    multasObtenidas = (from multa in contextoBaseDeDatos.Multa
                                       join prestamo in contextoBaseDeDatos.Prestamo
                                       on multa.FK_IdPrestamo equals prestamo.IdPrestamo
                                       where prestamo.FK_IdSocio == numeroSocio
                                       && multa.estado == Enumeradores.EnumeradorEstadoMulta.Pendiente.ToString()
                                       select multa).ToList();
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                multasObtenidas.Add(multaError);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                multasObtenidas.Add(multaError);
            }
            return multasObtenidas;
        }

        public int RegistrarPagoMultaPorId(int idMulta) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoRegistro = Constantes.ErrorEnLaOperacion;
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var multa = contextoBaseDeDatos.Multa.FirstOrDefault(entidad=>entidad.IdMulta==idMulta);
                    if (multa != null)
                    {
                        multa.estado = Enumeradores.EnumeradorEstadoMulta.Pagada.ToString();
                        multa.fechaPagoMulta = DateTime.Today;
                        resultadoRegistro=contextoBaseDeDatos.SaveChanges();
                    }
                    else 
                    {
                        resultadoRegistro = Constantes.SinResultadosEncontrados;
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
            return resultadoRegistro;
        }

        public int ActualizarMultas() 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoConsulta = Constantes.ErrorEnLaOperacion;
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    var actualizarEstadoQuery = @"UPDATE m
                                          SET m.Estado = 'Pendiente'
                                          FROM Multa m
                                          INNER JOIN Prestamo p ON m.FK_IdPrestamo = p.idPrestamo
                                          WHERE p.FechaDevolucionEsperada < CONVERT(DATE, GETDATE())
                                          AND p.estado <> 'Devuelto'
                                          AND m.Estado = 'Inactivo';";
                    contextoBaseDeDatos.Database.ExecuteSqlCommand(actualizarEstadoQuery);
                    var actualizarMontoQuery = @"
                                            UPDATE m
                                            SET m.MontoTotal = DATEDIFF(DAY, p.FechaDevolucionEsperada, GETDATE())
                                            FROM Multa m
                                            INNER JOIN Prestamo p ON m.FK_IdPrestamo = p.idPrestamo
                                            WHERE m.Estado = 'Pendiente';";
                    contextoBaseDeDatos.Database.ExecuteSqlCommand(actualizarMontoQuery);
                    resultadoConsulta = Constantes.OperacionExitosa;
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
            return resultadoConsulta;
        }
    }
}
