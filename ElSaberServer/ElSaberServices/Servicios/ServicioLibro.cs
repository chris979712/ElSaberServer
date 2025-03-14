using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberServices.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : ILibroManejador
    {
        public int AumentarCantidadLibrosDisponiblesPorISBN(string isbn)
        {
            LibroOperaciones libroOperaciones= new LibroOperaciones();            
            return libroOperaciones.AumentarNumeroLibrosDisponiblesPorISBN(isbn);
        }

        public List<EditorialBinding> ObtenerEditoriales()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Editorial> editorialesObtenidas=libroOperaciones.RecuperarEditorialesDeLaBaseDeDatos();
            List<EditorialBinding> editorialesRecuperadas = new List<EditorialBinding>();
            foreach (Editorial editorialObtenida in editorialesObtenidas) 
            {
                editorialesRecuperadas.Add(new EditorialBinding
                {
                    IdEditorial=editorialObtenida.IdEditorial,
                    Editorial=editorialObtenida.editorial1,
                });
            }
            return editorialesRecuperadas;
        }

        public int ObtenerIdLibroPorISBN(string isbn)
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();            
            return libroOperaciones.ObtenerIdLibroPorCodigoISBN(isbn);
        }

        public List<LibroBinding> ObtenerLibrosPorIdAutor(int idAutor)
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Libro> librosObtenidos = libroOperaciones.RecuperarLibrosPorIdAutor(idAutor);
            List<LibroBinding> librosRecibidos = new List<LibroBinding>();
            foreach (Libro libroObtenido in librosObtenidos) 
            {
                librosRecibidos.Add(new LibroBinding()
                {
                    Titulo=libroObtenido.titulo,
                    Isbn=libroObtenido.isbn,
                    FK_IdAutor=libroObtenido.FK_IdAutor,
                    FK_IdEditorial=libroObtenido.FK_IdEditorial,
                    FK_IdGenero=libroObtenido.FK_IdGenero,
                    AnioDePublicacion=libroObtenido.anioDePublicacion,
                    NumeroDePaginas=libroObtenido.numeroDePaginas,
                    RutaPortada=libroObtenido.rutaPortada,
                    Estado=libroObtenido.estado,
                    CantidadEjemplares=libroObtenido.cantidadEjemplares,
                    CantidadEjemplaresPrestados= (int)libroObtenido.cantidadEjemplaresPrestados,
                });
            }
            return librosRecibidos;
        }

        public List<LibroBinding> ObtenerLibrosPorIdGenero(int idGenero)
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Libro> librosObtenidos = libroOperaciones.RecuperarLibrosPorIdGenero(idGenero);
            List<LibroBinding> librosRecibidos = new List<LibroBinding>();
            foreach (Libro libroObtenido in librosObtenidos)
            {
                librosRecibidos.Add(new LibroBinding()
                {
                    Titulo = libroObtenido.titulo,
                    Isbn = libroObtenido.isbn,
                    FK_IdAutor = libroObtenido.FK_IdAutor,
                    FK_IdEditorial = libroObtenido.FK_IdEditorial,
                    FK_IdGenero = libroObtenido.FK_IdGenero,
                    AnioDePublicacion = libroObtenido.anioDePublicacion,
                    NumeroDePaginas = libroObtenido.numeroDePaginas,
                    RutaPortada = libroObtenido.rutaPortada,
                    Estado = libroObtenido.estado,
                    CantidadEjemplares = libroObtenido.cantidadEjemplares,
                    CantidadEjemplaresPrestados = (int)libroObtenido.cantidadEjemplaresPrestados,
                });
            }
            return librosRecibidos;
        }

        public List<LibroBinding> ObtenerLibrosPorISBN(string isbn)
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Libro> librosObtenidos = libroOperaciones.RecuperarLibrosPorISBN(isbn);
            List<LibroBinding> librosRecibidos = new List<LibroBinding>();
            foreach (Libro libroObtenido in librosObtenidos)
            {
                librosRecibidos.Add(new LibroBinding()
                {
                    Titulo = libroObtenido.titulo,
                    Isbn = libroObtenido.isbn,
                    FK_IdAutor = libroObtenido.FK_IdAutor,
                    FK_IdEditorial = libroObtenido.FK_IdEditorial,
                    FK_IdGenero = libroObtenido.FK_IdGenero,
                    AnioDePublicacion = libroObtenido.anioDePublicacion,
                    NumeroDePaginas = libroObtenido.numeroDePaginas,
                    RutaPortada = libroObtenido.rutaPortada,
                    Estado = libroObtenido.estado,
                    CantidadEjemplares = libroObtenido.cantidadEjemplares,
                    CantidadEjemplaresPrestados = (int)libroObtenido.cantidadEjemplaresPrestados,
                });
            }
            return librosRecibidos;
        }

        public List<LibroBinding> ObtenerLibrosPorTitulo(string titulo)
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Libro> librosObtenidos = libroOperaciones.RecuperarLibrosPorTitulo(titulo);
            List<LibroBinding> librosRecibidos = new List<LibroBinding>();
            foreach (Libro libroObtenido in librosObtenidos)
            {
                librosRecibidos.Add(new LibroBinding()
                {
                    Titulo = libroObtenido.titulo,
                    Isbn = libroObtenido.isbn,
                    FK_IdAutor = libroObtenido.FK_IdAutor,
                    FK_IdEditorial = libroObtenido.FK_IdEditorial,
                    FK_IdGenero = libroObtenido.FK_IdGenero,
                    AnioDePublicacion = libroObtenido.anioDePublicacion,
                    NumeroDePaginas = libroObtenido.numeroDePaginas,
                    RutaPortada = libroObtenido.rutaPortada,
                    Estado = libroObtenido.estado,
                    CantidadEjemplares = libroObtenido.cantidadEjemplares,
                    CantidadEjemplaresPrestados = (int)libroObtenido.cantidadEjemplaresPrestados,
                });
            }
            return librosRecibidos;
        }

        public List<AutorBinding> ObtenerListaDeAutores()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Autor> autoresObtenidos = libroOperaciones.RecuperarAutoresDeLaBaseDeDatos();
            List<AutorBinding> autoresRecuperados = new List<AutorBinding>();
            foreach (Autor autor in autoresObtenidos) 
            {
                autoresRecuperados.Add(new AutorBinding 
                {
                    Autor=autor.autor1,
                    IdAutor=autor.IdAutor,
                });
            }
            return autoresRecuperados;
        }

        public List<GeneroBinding> ObtenerListaDeGeneros()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Genero> generosObtenidos = libroOperaciones.RecuperarGenerosDeLaBaseDeDatos();
            List<GeneroBinding> generosRecuperados=new List<GeneroBinding>();
            foreach (Genero generoObtenido in generosObtenidos) 
            {
                generosRecuperados.Add(new GeneroBinding
                {
                    Genero=generoObtenido.genero1,
                    IdGenero=generoObtenido.IdGenero,
                });
            }
            return generosRecuperados;
        }

        public int RegistrarNuevoLibro(LibroBinding libro)
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            Libro nuevoLibro = new Libro 
            {
                titulo=libro.Titulo,
                isbn=libro.Isbn,
                FK_IdAutor=libro.FK_IdAutor,
                FK_IdEditorial=libro.FK_IdEditorial,
                FK_IdGenero=libro.FK_IdGenero,  
                anioDePublicacion=libro.AnioDePublicacion,
                numeroDePaginas=libro.NumeroDePaginas,
                rutaPortada=libro.RutaPortada,
                estado=libro.Estado,
                cantidadEjemplares=libro.CantidadEjemplares,
            };
            return libroOperaciones.RegistrarLibroEnLaBaseDeDatos(nuevoLibro);
        }

        public int ValidarExistenciaDeLibros()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            return libroOperaciones.ValidarExistenciaLibrosEnLaBaseDeDatos();
        }
    }
}
