﻿using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity; 
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
                    foreach (Multa multa1 in multasObtenidas) 
                    {
                        var prestamoAsociado = contextoBaseDeDatos.Prestamo.Where(entidad=>entidad.IdPrestamo==multa1.FK_IdPrestamo).FirstOrDefault();
                        if (prestamoAsociado != null)
                        {
                            multa1.Prestamo = prestamoAsociado;
                        }
                        else 
                        {
                            multasObtenidas.Insert(0, multaError);
                        }
                    }
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

        public List<Multa> ObtenerMultasPagadasEnDeterminadasFechas(string fechaInicioBusqueda, string fechaFinBusqueda)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Multa> multasObtenidas = new List<Multa>();
            Multa multaError = new Multa(){
                IdMulta = Constantes.ErrorEnLaOperacion
            };
            try
            {
                using(var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    DateTime fechaInicio = DateTime.Parse(fechaInicioBusqueda);
                    DateTime fechaFin = DateTime.Parse(fechaFinBusqueda);
                    var multas = contextoBaseDeDatos.Multa
                        .Include(m => m.Prestamo)
                        .Include(m => m.Prestamo.Socio)
                        .Include(m => m.Prestamo.Libro)
                        .Where(multa => multa.fechaPagoMulta <= fechaFin && multa.fechaPagoMulta >= fechaInicio)
                        .ToList();
                    if (multas.Count> 0 )
                    {
                        multasObtenidas = multas;
                    }
                    else
                    {
                        Multa multaSinCoincidencias = new Multa()
                        {
                            IdMulta = Constantes.SinResultadosEncontrados
                        };
                        multasObtenidas.Add(multaSinCoincidencias);
                    }
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
    }
}
