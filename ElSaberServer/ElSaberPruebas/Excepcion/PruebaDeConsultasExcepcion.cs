using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using ElSaberPruebas.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Excepcion
{
    public class PruebaDeConsultasExcepcion
    {
        /**
         * Pruebas de consulta de Acceso
         */
        [Fact]
        public void PruebaVerificarCredencialesExcepcion()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string usuario = "chrisvasquez985@gmail.com";
            string contraseniaEncriptada = Encriptado.hashToSHA2("contraseniasecreta123");
            int resultadoVerificacion = accesoOperaciones.VerificarCredenciales(usuario, contraseniaEncriptada);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoVerificacion);
        }

        [Fact]
        public void PruebaIniciarSesionEntityException()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            DatosUsuario datosUsuario = accesoOperaciones.IniciarSesion("ejemplo@prueba.com", "secreto123");
            Assert.True(datosUsuario.IdAcceso == -1);
        }

        /**
         * Pruebas de consulta de usuarios
         */
        [Fact]
        public void PruebaVerificarExistenciaDeUsuarioPruebaExcepcionExitosa()
        {
            UsuarioOperaciones usuarioOperacion = new UsuarioOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string telefono = "2281024672";
            int resultadoObtenido = usuarioOperacion.VerificarExistenciaDeUsuario(telefono, correo);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
         * Pruebas de consulta de usuarios
         */
        [Fact]
        public void PruebaObtenerSociosPorNombreExcepcionExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string nombre = "Pepe";
            List<Socio> sociosObtenidos = socioOperaciones.ObtenerSociosPorNombre(nombre);
            Assert.True(sociosObtenidos.Count == 3);
        }

        [Fact]
        public void PruebaConsultarSocioPorNumeroDeSocioExcepcionExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 100;
            Socio socioObtenido = socioOperaciones.ObtenerSocioPorNumeroDeSocio(numeroDeSocio);
            Assert.True(socioObtenido.numeroDeSocio == -1);
        }

        [Fact]
        public void PruebaVerificarSocioRegistradoEnLaBaseDeDatosExcepcionExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string telefonoABuscar = "123456789";
            int resultadoObtenido = socioOperaciones.VerificarSocioRegistradoEnLaBaseDeDatos(telefonoABuscar);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
         * Pruebas de consulta de libros
         */
        [Fact]
        public void PruebaValidarExistenciaLibrosEnLaBaseDeDatosExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int resultadoEsperado = -1;
            int resultadoObtenido = libroOperaciones.ValidarExistenciaLibrosEnLaBaseDeDatos();
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaObtenerIdLibroPorCodigoISBNExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            string isbn = "9781501142970";
            int resultadoObtenido = libroOperaciones.ObtenerIdLibroPorCodigoISBN(isbn);
            Assert.False(resultadoObtenido > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorTituloExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            string titulo = "Eso";
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorTitulo(titulo);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.False(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorISBNExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            string isbn = "9781501142970";
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorISBN(isbn);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.False(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorIdAutorExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int idAutor = 1;
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorIdAutor(idAutor);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.False(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorIdGeneroExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int idGenero = 1;
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorIdGenero(idGenero);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.False(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarGenerosDeLaBaseDeDatosExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Genero> generosObtenidos = libroOperaciones.RecuperarGenerosDeLaBaseDeDatos();
            Genero genero = generosObtenidos.FirstOrDefault();
            Assert.False(genero.IdGenero > 0);
        }

        [Fact]
        public void PruebaRecuperarAutoresDeLaBaseDeDatosExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Autor> autoresObtenidos = libroOperaciones.RecuperarAutoresDeLaBaseDeDatos();
            Autor autor = autoresObtenidos.FirstOrDefault();
            Assert.False(autor.IdAutor > 0);
        }

        [Fact]
        public void PruebaRecuperarEditorialesDeLaBaseDeDatosExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Editorial> editorialesObtenidas = libroOperaciones.RecuperarEditorialesDeLaBaseDeDatos();
            Editorial editorial = editorialesObtenidas.FirstOrDefault();
            Assert.False(editorial.IdEditorial > 0);
        }

        [Fact]
        public void PruebaObtenerLibrosMasPrestadosPorFechaExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<LibroMasPrestado> libroMasPrestadosObtenidos = libroOperaciones.ObtenerLibrosMasPrestadosPorFecha("2025-03-29", "2025-04-28");
            string cantidadEjemplaresPrestados = "-1";
            Assert.Equal(libroMasPrestadosObtenidos[0].cantidadDeEjemplares, cantidadEjemplaresPrestados);
        }

        [Fact]
        public void PruebaObtenerInventarioDeLibrosExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<InventarioLibro> inventarioLibrosObtenido = libroOperaciones.ObtenerInventarioLibros();
            Assert.Equal(-1, inventarioLibrosObtenido[0].cantidadTotal);
        }

        /**
         * Pruebas de consulta de prestamos
         */
        [Fact]
        public void PruebaRecuperarPrestamosActivosYVencidosPorNumeroSocioExcepcionExitosa()
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int numeroSocio = 1;
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosYVencidosPorNumeroSocio(numeroSocio);
            Prestamo prestamo = prestamosObtenidos.FirstOrDefault();
            Assert.False(prestamo.IdPrestamo > 0);
        }


    }
}
