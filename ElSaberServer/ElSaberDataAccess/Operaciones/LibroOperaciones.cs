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

        public List<Libro> RecuperarLibrosPorTitulo(string titulo) 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Libro> librosObtenidos= new List<Libro>();
            Libro libro = new Libro()
            {
                IdLibro = Constantes.ErrorEnLaOperacion,
            };
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()) 
                {
                    librosObtenidos = contextoBaseDeDatos.Libro.Where(entidad => entidad.titulo.Contains(titulo)).ToList();
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
                librosObtenidos.Add(libro);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                librosObtenidos.Add(libro);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                librosObtenidos.Add(libro);
            }
            return librosObtenidos;
        }

        public List<Libro> RecuperarLibrosPorISBN(string isbn)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Libro> librosObtenidos = new List<Libro>();
            Libro libro = new Libro()
            {
                IdLibro = Constantes.ErrorEnLaOperacion,
            };
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    librosObtenidos = contextoBaseDeDatos.Libro.Where(entidad => entidad.isbn.Contains(isbn)).ToList();
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
                librosObtenidos.Add(libro);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                librosObtenidos.Add(libro);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                librosObtenidos.Add(libro);
            }
            return librosObtenidos;
        }

        public List<Libro> RecuperarLibrosPorIdAutor(int idAutor)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Libro> librosObtenidos = new List<Libro>();
            Libro libro = new Libro()
            {
                IdLibro = Constantes.ErrorEnLaOperacion,
            };
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    librosObtenidos = contextoBaseDeDatos.Libro.Where(entidad => entidad.FK_IdAutor==idAutor).ToList();
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
                librosObtenidos.Add(libro);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                librosObtenidos.Add(libro);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                librosObtenidos.Add(libro);
            }
            return librosObtenidos;
        }

        public List<Libro> RecuperarLibrosPorIdGenero(int idGenero)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Libro> librosObtenidos = new List<Libro>();
            Libro libro = new Libro()
            {
                IdLibro = Constantes.ErrorEnLaOperacion,
            };
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    librosObtenidos = contextoBaseDeDatos.Libro.Where(entidad => entidad.FK_IdGenero == idGenero).ToList();
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
                librosObtenidos.Add(libro);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                librosObtenidos.Add(libro);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                librosObtenidos.Add(libro);
            }
            return librosObtenidos;
        }

        public List<Genero> RecuperarGenerosDeLaBaseDeDatos()
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Genero> generosObtenidos = new List<Genero>();
            Genero genero = new Genero()
            {
                IdGenero = Constantes.ErrorEnLaOperacion,
            };
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities()) 
                {
                    generosObtenidos=contextoBaseDeDatos.Genero.ToList();
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
                generosObtenidos.Add(genero);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                generosObtenidos.Add(genero);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                generosObtenidos.Add(genero);
            }
            return generosObtenidos;
        }

        public List<Autor> RecuperarAutoresDeLaBaseDeDatos()
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Autor> autoresObtenidos = new List<Autor>();
            Autor autor = new Autor()
            {
                IdAutor = Constantes.ErrorEnLaOperacion,
            };
            try
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    autoresObtenidos = contextoBaseDeDatos.Autor.ToList();
                }
            }
            catch (DbUpdateException dbUpdateException)
            {
                logger.LogWarn(dbUpdateException);
                autoresObtenidos.Add(autor);
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                autoresObtenidos.Add(autor);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                autoresObtenidos.Add(autor);
            }
            return autoresObtenidos;
        }

        public List<Editorial> RecuperarEditorialesDeLaBaseDeDatos() 
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            List<Editorial> editorialesObtenidas = new List<Editorial>();
            Editorial editorial = new Editorial 
            {
                IdEditorial=Constantes.ErrorEnLaOperacion,
            };
            try 
            {
                using (var contextoBaseDeDatos = new ElSaberDBEntities())
                {
                    editorialesObtenidas = contextoBaseDeDatos.Editorial.ToList();
                }
            }
            catch (SqlException sqlException)
            {
                logger.LogError(sqlException);
                editorialesObtenidas.Add(editorial);
            }
            catch (EntityException entityException)
            {
                logger.LogFatal(entityException);
                editorialesObtenidas.Add(editorial);
            }
            return editorialesObtenidas;
        }
    }
}
