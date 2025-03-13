using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberPruebas.Utilities;
using System;
using System.Collections.Generic;
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
    }

    public class DatabaseFixtureQuery : IDisposable
    {
        public DatabaseFixtureQuery()
        {
            //InsertarSociosPruebaDeConsulta();
        }

        public void InsertarSociosPruebaDeConsulta()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            Socio socioNuevoPrueba = new Socio()
            {
                nombre = "Pepe",
                primerApellido = "Vela",
                segundoApellido = "Torres",
                telefono = "123456789",
                fechaInscripcion = DateTime.Now,
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
