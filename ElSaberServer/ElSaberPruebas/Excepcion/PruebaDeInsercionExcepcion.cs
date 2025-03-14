using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Excepcion
{
    public class PruebaDeInsercionExcepcion
    {

        /**
         * Pruebas de registros de usuarios con excepcion
         */
        [Fact]
        public void PruebaRegistrarUsuarioEnLaBaseDeDatosExitosa()
        {
            Direccion direccion = new Direccion()
            {
                calle = "Juarez",
                numero = "57",
                codigoPostal = "91000",
                ciudad = "Xalapa"
            };

            Usuario usuario = new Usuario()
            {
                nombre = "Christopher",
                primerApellido = "Vasquez",
                segundoApellido = "Zapata",
                telefono = "2281024672",
                puesto = "Administrado"
            };

            Acceso acceso = new Acceso()
            {
                correo = "chrisvasquez985@gmail.com",
                contrasenia = "contraseniasecreta123",
                tipoDeUsuario = "Administrado"
            };
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int resultadoObtenido = usuarioOperaciones.RegistrarUsuarioEnLaBaseDeDatos(usuario, acceso, direccion);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


        /**
         * Pruebas de registros de direccion con excepcion
         */
        [Fact]
        public void PruebaRegistrarNuevaDireccion()
        {
            Direccion direccion = new Direccion()
            {
                calle = "Jacarandas",
                numero = "15",
                codigoPostal = "91876",
                ciudad = "Xalapa"
            };
            DireccionOperaciones direccionOperaciones = new DireccionOperaciones();
            int resultadoObtenido = direccionOperaciones.AgregarNuevaDireccion(direccion);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }


        /**
         * Pruebas de registros de socios con excepcion
         */
        [Fact]
        public void PruebaRegistrarNuevoSocioExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            Socio socioNuevoPrueba = new Socio()
            {
                nombre = "Maria",
                primerApellido = "Rodriguez",
                segundoApellido = "Acosta",
                telefono = "123456789",
                fechaInscripcion = DateTime.Now,
            };
            Direccion direccionNuevaSocio = new Direccion()
            {
                calle = "Jardines",
                numero = "1",
                codigoPostal = "91100",
                ciudad = "Xalapa"
            };
            int resultadoObtenido = socioOperaciones.RegistrarNuevoSocio(socioNuevoPrueba, direccionNuevaSocio);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
        * Pruebas de registros de Libros
        *
        */
        [Fact]
        public void PruebaRegistrarLibroEnLaBaseDeDatosExcepcionExitosa()
        {
            Libro libro = new Libro
            {
                titulo = "Eso",
                isbn = "9781501142970",
                FK_IdAutor = 1,
                FK_IdEditorial = 1,
                FK_IdGenero = 1,
                anioDePublicacion = "1986",
                numeroDePaginas = "1135",
                rutaPortada = "eso.jpg",
                estado = Enumeradores.EnumeradorEstadoLibro.Disponible.ToString(),
                cantidadEjemplares = 1,
                cantidadEjemplaresPrestados = Constantes.ValorPorDefecto,
            };
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int resultadoEsperado = -1;
            int resultadoObtenido = libroOperaciones.RegistrarLibroEnLaBaseDeDatos(libro);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaAumentarNumeroLibrosDisponiblesPorISBNExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            string isbn = "9781501142970";
            int resultadoEsperado = -1;
            int resultadoObtenido = libroOperaciones.AumentarNumeroLibrosDisponiblesPorISBN(isbn);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
        * Pruebas de registros de Prestamos
        *
        */
        [Fact]
        public void PruebaRegistrarPrestamoEnLaBaseDeDatosExcepcionExitosa()
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int resultadoEsperado = -1;
            Prestamo prestamo = new Prestamo()
            {
                fechaPrestamo = DateTime.Today,
                fechaDevolucionEsperada = DateTime.Today.AddDays(7),
                nota = "Buen libro",
                FK_IdLibro = 1,
                FK_IdSocio = 1,
                FK_IdUsuario = 1,
            };
            int resultadoObtenido = prestamoOperaciones.RegistrarPrestamoEnLaBaseDeDatos(prestamo);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
        * Pruebas de registros de Devolucion
        *
        */
        [Fact]
        public void PruebaRegistrarDevolucionEnLaBaseDeDatosExitosa()
        {
            DevolucionOperaciones devolucionOperaciones = new DevolucionOperaciones();
            int resultadoEsperado = -1;
            Devolucion devolucion = new Devolucion()
            {
                FK_IdPrestamo = 1,
                fechaDevolucion = DateTime.Today,
                nota = "Nada",
                estadoLibro = Enumeradores.EnumeradorEstadoLibro.Disponible.ToString(),
            };
            int resultadoObtenido = devolucionOperaciones.RegistrarDevolucionEnLaBaseDeDatos(devolucion);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}