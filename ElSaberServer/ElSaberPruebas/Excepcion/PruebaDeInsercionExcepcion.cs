using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
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
    }
}