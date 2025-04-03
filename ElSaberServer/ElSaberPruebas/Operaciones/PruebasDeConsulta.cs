using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using ElSaberPruebas.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace ElSaberServerTest.Operaciones
{

    public class PruebasDeConsulta : IClassFixture<DatabaseFixtureQuery>
    {

        /**
         * Pruebas de consulta de Acceso
         */
        [Fact]
        public void PruebaVerificarCredencialesExitosa()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string usuario = "chrisvasquez985@gmail.com";
            string contraseniaEncriptada ="contraseniasecreta123"; 
            int resultadoVerificacion = accesoOperaciones.VerificarCredenciales(usuario, contraseniaEncriptada);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoVerificacion);
        }

        [Fact]
        public void PruebaVerificarCredencialesInexistentesExitosa()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string usuario = "chrisvasquez985@gmail.com";
            string contraseniaEncriptada = Encriptado.hashToSHA2("secreto123");
            int resultadoVerificacion = accesoOperaciones.VerificarCredenciales(usuario, contraseniaEncriptada);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoVerificacion);
        }

        [Fact]
        public void PruebaVerificarCorreoExistente()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string usuario = "chrisvasquez985@gmail.com";
            int resultadoVerificacion = accesoOperaciones.VerificarCorreoExistente(usuario);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoVerificacion);
        }

        [Fact]
        public void PruebaVerificarCorreoInexistente()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string usuario = "chrisvasquez999@gmail.com";
            int resultadoVerificacion = accesoOperaciones.VerificarCorreoExistente(usuario);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoVerificacion);
        }

        [Fact]
        public void PruebaIniciarSesionExitosa()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string usuario = "chrisvasquez985@gmail.com";
            string contraseniaEncriptada = Encriptado.hashToSHA2("contraseniasecreta123");
            DatosUsuario usuarioObtenido = accesoOperaciones.IniciarSesion(usuario, contraseniaEncriptada);
            int resultadoOperacion = -1;
            Assert.NotNull(usuarioObtenido);
            Assert.NotEqual(resultadoOperacion, usuarioObtenido.IdAcceso);
        }

        [Fact]
        public void PruebaIniciarSesionCorreoIncorrecto()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string correo = "chrisvasquez999@gmail.com";
            string contraseniaEncriptada = Encriptado.hashToSHA2("contraseniasecreta123");
            DatosUsuario usuarioObtenido = accesoOperaciones.IniciarSesion(correo, contraseniaEncriptada);
            int resultadoEsperado = 0;
            Assert.NotNull(usuarioObtenido);
            Assert.Equal(resultadoEsperado, usuarioObtenido.IdAcceso);
        }

        [Fact]
        public void PruebaIniciarSesionContraseniaIncorrecta()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string contraseniaEncriptada = Encriptado.hashToSHA2("secreto123");
            DatosUsuario usuarioObtenido = accesoOperaciones.IniciarSesion(correo, contraseniaEncriptada);
            int resultadoEsperado = 0;
            Assert.NotNull(usuarioObtenido);
            Assert.Equal(resultadoEsperado, usuarioObtenido.IdAcceso);
        }

        /**
         * Pruebas de consulta de usuarios
         */
        [Fact]
        public void PruebaVerificarExistenciaDeUsuarioPruebaExitosa()
        {
            UsuarioOperaciones usuarioOperacion = new UsuarioOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string telefono = "2281024672";
            int resultadoObtenido = usuarioOperacion.VerificarExistenciaDeUsuario(telefono, correo);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaVerificarExistenciaDeUsuarioSinExistirPruebaExitosa()
        {
            UsuarioOperaciones usuarioOperacion = new UsuarioOperaciones();
            string correo = "chrisvasquez999@gmail.com";
            string telefono = "2281024664";
            int resultadoObtenido = usuarioOperacion.VerificarExistenciaDeUsuario(telefono, correo);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
         * Pruebas de consulta de socios
         */
        [Fact]
        public void PruebaObtenerSociosPorNombreUnSoloNombreExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string nombre = "Pepe";
            List<Socio> sociosObtenidos = socioOperaciones.ObtenerSociosPorNombre(nombre);
            Assert.True(sociosObtenidos.Count == 3);
        }

        [Fact]
        public void PruebaObtenerSociosPorNombreUnNombreYApellidoExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string nombre = "Pepe Rodriguez";
            List<Socio> sociosObtenidos = socioOperaciones.ObtenerSociosPorNombre(nombre);
            Assert.True(sociosObtenidos.Count == 2);
        }

        [Fact]
        public void PruebaObtenerSociosPorNombreUnNombreYDosApellidoExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string nombre = "Pepe Rodriguez Gonzalez";
            List<Socio> sociosObtenidos = socioOperaciones.ObtenerSociosPorNombre(nombre);
            Assert.True(sociosObtenidos.Count == 1);
        }

        [Fact]
        public void PruebaObtenerSociosPorNombreSinExistenciasExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string nombre = "Oscar Apodaca García";
            List<Socio> sociosObtenidos = socioOperaciones.ObtenerSociosPorNombre(nombre);
            Socio socioObtenido = sociosObtenidos[0];
            Assert.True(socioObtenido.numeroDeSocio==0);
        }

        [Fact]
        public void PruebaConsultarSocioPorNumeroDeSocioExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 2;
            Socio socioObtenido = socioOperaciones.ObtenerSocioPorNumeroDeSocio(numeroDeSocio);
            string nombreEsperado = "Pepe";
            Assert.Equal(nombreEsperado,socioObtenido.nombre);
        }

        [Fact]
        public void PruebaConsultarSocioPorNumeroDeSocioSinExitirExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 100;
            Socio socioObtenido = socioOperaciones.ObtenerSocioPorNumeroDeSocio(numeroDeSocio);
            Assert.True(socioObtenido.numeroDeSocio == 0);
        }

        [Fact]
        public void PruebaVerificarSocioRegistradoEnLaBaseDeDatosExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string telefonoABuscar = "123456789";
            int resultadoObtenido = socioOperaciones.VerificarSocioRegistradoEnLaBaseDeDatos(telefonoABuscar);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaVerificarSocioSinExistenciaEnLaBaseDeDatosExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            string telefonoABuscar = "9987651232";
            int resultadoObtenido = socioOperaciones.VerificarSocioRegistradoEnLaBaseDeDatos(telefonoABuscar);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
         * Pruebas de consulta de libros
         */
        [Fact]
        public void PruebaValidarExistenciaLibrosEnLaBaseDeDatosExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int resultadoEsperado = 1;
            int resultadoObtenido = libroOperaciones.ValidarExistenciaLibrosEnLaBaseDeDatos();
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaObtenerIdLibroPorCodigoISBNExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            string isbn = "9781501142970";
            int resultadoObtenido = libroOperaciones.ObtenerIdLibroPorCodigoISBN(isbn);
            Assert.True(resultadoObtenido > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorTituloExitosa() 
        {
            LibroOperaciones libroOperaciones=new LibroOperaciones();            
            string titulo = "Eso";
            List <Libro> librosObtenidos=new List<Libro>();
            librosObtenidos=libroOperaciones.RecuperarLibrosPorTitulo(titulo);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.True(libro.IdLibro>0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorISBNExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            string isbn = "9781501142970";
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorISBN(isbn);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.True(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorIdAutorExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int idAutor = 1;
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorIdAutor(idAutor);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.True(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarLibrosPorIdGeneroExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int idGenero = 1;
            List<Libro> librosObtenidos = new List<Libro>();
            librosObtenidos = libroOperaciones.RecuperarLibrosPorIdGenero(idGenero);
            Libro libro = librosObtenidos.FirstOrDefault();
            Assert.True(libro.IdLibro > 0);
        }

        [Fact]
        public void PruebaRecuperarGenerosDeLaBaseDeDatosExitosa() 
        {
            LibroOperaciones libroOperaciones=new LibroOperaciones();
            List<Genero> generosObtenidos=libroOperaciones.RecuperarGenerosDeLaBaseDeDatos();
            Genero genero = generosObtenidos.FirstOrDefault();
            Assert.True(genero.IdGenero>0);
        }

        [Fact]
        public void PruebaRecuperarAutoresDeLaBaseDeDatosExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Autor> autoresObtenidos = libroOperaciones.RecuperarAutoresDeLaBaseDeDatos();
            Autor autor = autoresObtenidos.FirstOrDefault();
            Assert.True(autor.IdAutor > 0);
        }

        [Fact]
        public void PruebaRecuperarEditorialesDeLaBaseDeDatosExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<Editorial> editorialesObtenidas = libroOperaciones.RecuperarEditorialesDeLaBaseDeDatos();
            Editorial editorial = editorialesObtenidas.FirstOrDefault();
            Assert.True(editorial.IdEditorial > 0);
        }

        [Fact]
        public void PruebaValidarLibroDisponiblePorIdLibroExitosa() 
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int idLibro = 2;
            int resultadoEsperado = 1;
            int resultadoObtenido = libroOperaciones.ValidarLibroDisponiblePorIdLibro(idLibro);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaValidarExistenciaPrestamosVencidosPorNumeroSocioExitosa() 
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int numeroSocio = 1;
            int resultadoEsperado = 0;
            int resultadoObtenido = prestamoOperaciones.ValidarExistenciaPrestamosVencidosPorNumeroSocio(numeroSocio);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaObtenerTituloLibroPorIdExitosa() 
        {
            LibroOperaciones libroOperaciones=new LibroOperaciones();
            int idLibro = 1;
            string resultadoEsperado = "Eso";
            string resultadoObtenido = libroOperaciones.ObtenerTituloLibroPorId(idLibro);
            Assert.Equal(resultadoEsperado,resultadoObtenido);            
        }

        [Fact]
        public void PruebaObtenerLibrosMasPrestadosPorFechaExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<LibroMasPrestado> libroMasPrestados = new List<LibroMasPrestado>();
            LibroMasPrestado libroMasPrestado1 = new LibroMasPrestado()
            {
                titulo = "El principito",
                isbn = "9781501142970",
                autor = "George R.R. Martin",
                genero = "No ficción",
                cantidadDeEjemplares = "12"
            };
            LibroMasPrestado libroMasPrestado2 = new LibroMasPrestado()
            {
                titulo = "Eso",
                isbn = "1111111111111",
                autor = "George R.R. Martin",
                genero = "Terror",
                cantidadDeEjemplares = "1"
            };
            libroMasPrestados.Add(libroMasPrestado1);
            libroMasPrestados.Add(libroMasPrestado2);
            List<LibroMasPrestado> libroMasPrestadosObtenidos = libroOperaciones.ObtenerLibrosMasPrestadosPorFecha("2025-03-26", "2025-03-28");
            Assert.Equal(libroMasPrestados.Count, libroMasPrestadosObtenidos.Count);
        }

        [Fact]
        public void PruebaObtenerLibrosMasPrestadosPorFechaSinActividadExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<LibroMasPrestado> libroMasPrestadosObtenidos = libroOperaciones.ObtenerLibrosMasPrestadosPorFecha("2025-03-29", "2025-04-28");
            string cantidadEjemplaresPrestados = "0";
            Assert.Equal(libroMasPrestadosObtenidos[0].cantidadDeEjemplares, cantidadEjemplaresPrestados);
        }

        [Fact]
        public void PruebaObtenerInventarioDeLibrosExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<InventarioLibro> inventarioLibros = new List<InventarioLibro>();
            InventarioLibro libroInventario1 = new InventarioLibro()
            {
                tituloLibro = "El principito",
                ISBN = "9781501142970",
                cantidadDisponible = 8,
                cantidadNoDisponible = 2,
                cantidadTotal = 10
            };
            InventarioLibro libroInventario2 = new InventarioLibro()
            {
                tituloLibro = "Eso",
                ISBN = "1111111111111",
                cantidadDisponible = 0,
                cantidadNoDisponible = 1,
                cantidadTotal = 1
            };
            inventarioLibros.Add(libroInventario1);
            inventarioLibros.Add(libroInventario2);
            List<InventarioLibro> inventarioLibrosObtenido = libroOperaciones.ObtenerInventarioLibros();
            Assert.Equal(inventarioLibros.Count,inventarioLibrosObtenido.Count);
        }

        /**
         * Pruebas de consulta de prestamos
         */
        [Fact]
        public void PruebaRecuperarPrestamosActivosYVencidosPorNumeroSocioExitosa() 
        {
            PrestamoOperaciones prestamoOperaciones=new PrestamoOperaciones();
            int numeroSocio = 1;
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosYVencidosPorNumeroSocio(numeroSocio);
            Prestamo prestamo = prestamosObtenidos.FirstOrDefault();
            Assert.True(prestamo.IdPrestamo > 0);
        }

        [Fact]
        public void PruebaRecuperarTodosLosPrestamosPorNumeroSocioExitosa() 
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int numeroSocio = 1;
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarTodosLosPrestamosPorNumeroSocio(numeroSocio);
            Prestamo prestamo = prestamosObtenidos.FirstOrDefault();
            Assert.True(prestamo.IdPrestamo > 0);
        }

        [Fact]
        public void PruebaRecuperarTodosLosPrestamosPorNumeroSocioFallido()
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int numeroSocio = 1000;
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarTodosLosPrestamosPorNumeroSocio(numeroSocio);
            Assert.True(prestamosObtenidos.Count == 0);
        }

        [Fact]
        public void PruebaRecuperarPrestamosActivosPorNumeroSocioExitosa()
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int numeroSocio = 1;
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosPorNumeroSocio(numeroSocio);
            Prestamo prestamo = prestamosObtenidos.FirstOrDefault();
            Assert.True(prestamo.IdPrestamo > 0);
        }

        [Fact]
        public void PruebaRecuperarPrestamosActivosPorISBNExitosa() 
        {
            PrestamoOperaciones prestamoOperaciones= new PrestamoOperaciones();
            string isbn = "9781501142970";
            List<Prestamo> prestamosObtenidos=prestamoOperaciones.RecuperarPrestamosActivosPorISBN(isbn);
            Prestamo prestamo = prestamosObtenidos.FirstOrDefault();
            Assert.True(prestamo.IdPrestamo > 0);
        }

        [Fact]
        public void PruebaRecuperarPrestamosActivosPorISBNFallida()
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            string isbn = "1234567890123";
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosPorISBN(isbn);            
            Assert.True(prestamosObtenidos.Count()==0);
        }

        [Fact]
        public void PruebaRecuperarPrestamosActivosPorFechaInicioExitosa() 
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            DateTime fechaInicio = DateTime.Parse("2025-03-26");
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosPorFechaInicio(fechaInicio);
            Prestamo prestamo = prestamosObtenidos.FirstOrDefault();
            Assert.True(prestamo.IdPrestamo > 0);
        }

        [Fact]
        public void PruebaRecuperarPrestamosActivosPorFechaInicioFallida()
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            DateTime fechaInicio = DateTime.Parse("2020-03-26");
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosPorFechaInicio(fechaInicio);
            Assert.True(prestamosObtenidos.Count() == 0);
        }

        /**
         * Pruebas de consulta de Multas
         */
        [Fact]
        public void PruebaRecuperarMultasPendientesPorNumeroSocioExitosa() 
        {
            MultaOperaciones multaOperaciones=new MultaOperaciones();
            int numeroSocio = 1;            
            List<Multa> multasObtenidas=multaOperaciones.RecuperarMultasPendientesPorNumeroSocio(numeroSocio);
            Multa multa=multasObtenidas.FirstOrDefault();
            Assert.True(multa.IdMulta>0);
        }

        [Fact]
        public void PruebaRecuperarMultasPendientesPorNumeroSocioFallida()
        {
            MultaOperaciones multaOperaciones = new MultaOperaciones();
            int numeroSocio = 9999;
            List<Multa> multasObtenidas = multaOperaciones.RecuperarMultasPendientesPorNumeroSocio(numeroSocio);            
            Assert.True(multasObtenidas.Count==0);
        }
    }

    public class DatabaseFixtureQuery : IDisposable
    {
        public DatabaseFixtureQuery()
        {
            InsertarSociosPruebaDeConsulta();
            //InsertarPrestamosYLibros();
        }

        public void InsertarPrestamosYLibros()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            Prestamo prestamo = new Prestamo()
            {
                fechaPrestamo = DateTime.Today,
                fechaDevolucionEsperada = DateTime.Today.AddDays(7),
                nota = "Buen libro",
                FK_IdLibro = 1,
                FK_IdSocio = 1,
                FK_IdUsuario = 1,
            };
            Prestamo prestamo2 = new Prestamo()
            {
                fechaPrestamo = DateTime.Today,
                fechaDevolucionEsperada = DateTime.Today.AddDays(7),
                nota = "Buen libro",
                FK_IdLibro = 1,
                FK_IdSocio = 1,
                FK_IdUsuario = 1,
            };
            Prestamo prestamo3 = new Prestamo()
            {
                fechaPrestamo = DateTime.Today,
                fechaDevolucionEsperada = DateTime.Today.AddDays(7),
                nota = "Buen libro",
                FK_IdLibro = 2,
                FK_IdSocio = 1,
                FK_IdUsuario = 1,
            };
            prestamoOperaciones.RegistrarPrestamoEnLaBaseDeDatos(prestamo);
            prestamoOperaciones.RegistrarPrestamoEnLaBaseDeDatos(prestamo2);
            prestamoOperaciones.RegistrarPrestamoEnLaBaseDeDatos(prestamo3);
        }

        public void InsertarSociosPruebaDeConsulta()
        {
            DateTime fechaDeNacimiento;
            bool fecha = DateTime.TryParse("2004-09-12", out fechaDeNacimiento);
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            Socio socioNuevoPrueba = new Socio()
            {
                nombre = "Pepe",
                primerApellido = "Vela",
                segundoApellido = "Torres",
                telefono = "123456789",
                fechaInscripcion = DateTime.Now,
                fechaNacimiento = fechaDeNacimiento,
            };
            Direccion direccionNuevaSocio = new Direccion()
            {
                calle = "Lucio",
                numero = "27",
                codigoPostal = "91000",
                ciudad = "Xalapa"
            };
            Socio socioNuevoPruebaDos = new Socio()
            {
                nombre = "Pepe",
                primerApellido = "Rodriguez",
                segundoApellido = "Jimenez",
                telefono = "987654321",
                fechaInscripcion = DateTime.Now,
                fechaNacimiento = fechaDeNacimiento,
            };
            Direccion direccionNuevaSocioDos = new Direccion()
            {
                calle = "Juarez",
                numero = "51",
                codigoPostal = "91000",
                ciudad = "Xalapa"
            };
            Socio socioNuevoPruebaTres = new Socio()
            {
                nombre = "Pepe",
                primerApellido = "Rodriguez",
                segundoApellido = "Gonzalez",
                telefono = "1122334455",
                fechaInscripcion = DateTime.Now,
                fechaNacimiento = fechaDeNacimiento,
            };
            Direccion direccionNuevaSocioTres = new Direccion()
            {
                calle = "Jacarandas",
                numero = "23",
                codigoPostal = "91800",
                ciudad = "Xalapa"
            };
            socioOperaciones.RegistrarNuevoSocio(socioNuevoPrueba, direccionNuevaSocio);
            socioOperaciones.RegistrarNuevoSocio(socioNuevoPruebaDos, direccionNuevaSocioDos);
            socioOperaciones.RegistrarNuevoSocio(socioNuevoPruebaTres, direccionNuevaSocioTres);
        }

        public void Dispose()
        {
            Console.WriteLine("All test executed");
        }
    }
}
