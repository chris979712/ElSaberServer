using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberPruebas.Utilities;

namespace ElSaberServerTest.Operaciones
{

    public class PruebasDeInsercion
    {

        /**
         * Pruebas de registros de usuarios
         *
         */

        [Fact]
        public void PruebaRegistrarUsuarioEnLaBaseDeDatosExitosa()
        {
            string contraseniaEncriptada = Encriptado.hashToSHA2("contraseniasecreta123");
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
                puesto = "Administrador"
            };

            Acceso acceso = new Acceso()
            {
                correo = "chrisvasquez985@gmail.com",
                contrasenia = contraseniaEncriptada,
                tipoDeUsuario = "Administrado"
            };
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int resultadoObtenido = usuarioOperaciones.RegistrarUsuarioEnLaBaseDeDatos(usuario, acceso, direccion);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
        * Pruebas de registros de direccion
        *
        */

        [Fact]
        public void PruebaRegistrarNuevaDireccionExitosa()
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
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
        * Pruebas de registros de Socios
        *
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
            Assert.Equal(resultadoEsperado,resultadoObtenido);
        }

    }
}
