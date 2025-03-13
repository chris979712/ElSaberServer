using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
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
                    else 
                    {
                        idLibro = Constantes.ValorPorDefecto;
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

        public int RegistrarLibroEnLaBaseDeDatos(Libro libro) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.ErrorEnLaOperacion;            
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()) 
                {                    
                    var nuevoLibro = new Libro()
                    {
                        titulo = libro.titulo,
                        isbn = libro.isbn,
                        FK_IdAutor = libro.FK_IdAutor,
                        FK_IdEditorial = libro.FK_IdEditorial,
                        FK_IdGenero = libro.FK_IdGenero,
                        anioDePublicacion = libro.anioDePublicacion,
                        numeroDePaginas = libro.numeroDePaginas,
                        rutaPortada = libro.rutaPortada,
                        estado = Enumeradores.EnumeradorEstadoLibro.Disponible.ToString(),
                        cantidadEjemplares = libro.cantidadEjemplares,
                        cantidadEjemplaresPrestados = Constantes.ValorPorDefecto,
                    };
                    contextoBaseDeDatos.Libro.Add(nuevoLibro);
                    resultadoInsercion=contextoBaseDeDatos.SaveChanges();                                                       
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

        public int AumentarNumeroLibrosDisponiblesPorISBN(string isbn) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoInsercion = Constantes.ErrorEnLaOperacion;
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()) 
                {
                    var libro = contextoBaseDeDatos.Libro.FirstOrDefault(entidad => entidad.isbn == isbn);
                    if (libro != null)
                    {
                        libro.cantidadEjemplares += 1;
                        resultadoInsercion = contextoBaseDeDatos.SaveChanges();
                    }
                    else 
                    {
                        resultadoInsercion = Constantes.ValorPorDefecto;
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
            return resultadoInsercion;
        }
    }
}
