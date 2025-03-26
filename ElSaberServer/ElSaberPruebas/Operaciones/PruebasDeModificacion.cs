using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace ElSaberServerTest.Operaciones
{
	
	public class PruebasDeModificacion
	{
        /**
         * Prueba Modificacion Acceso
         */
        [Fact]
        public void PruebaModificacionAccesoExitosa()
        {
            AccesoOperaciones acceso = new AccesoOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoModificacion = acceso.ModificarContrasenia(correo,contrasenia);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificacionAccesoInexistenteExitosa()
        {
            AccesoOperaciones acceso = new AccesoOperaciones();
            string correo = "chris9999@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoModificacion = acceso.ModificarContrasenia(correo, contrasenia);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        /**
         * Prueba Modificacion Socio
         */
        [Fact]
        public void PruebaModificarEstadoSocioExistenteExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 1;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        [Fact]
        public void PruebaModificarEstadoSocioNoExistenteExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 91291;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        [Fact]
        public void PruebaModificarDatosSocioExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 1;
            DateTime fechaInscipcion = DateTime.Parse("2025-04-12");
            DateTime fechaNacimiento = DateTime.Parse("2008-06-12");
            Socio socio = new Socio()
            {
                nombre = "Juan",
                primerApellido = "Cumplido",
                segundoApellido = "Negrete",
                telefono = "2281001122",
                fechaInscripcion = fechaInscipcion,
                fechaNacimiento = fechaNacimiento,
                Direccion = new Direccion()
                {
                    calle = "Av.Xalapa",
                    codigoPostal = 91101.ToString(),
                    ciudad = "Xalapa"
                }
            };
            int resultadoEsperado = 1;
            int resultadoModificacion = socioOperaciones.EditarDatosDeSocio(numeroDeSocio,socio);
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificarDatosSocioSinExistenciaExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 919191;
            DateTime fechaInscipcion = DateTime.Parse("2025-04-12");
            DateTime fechaNacimiento = DateTime.Parse("2008-06-12");
            Socio socio = new Socio()
            {
                nombre = "Juan",
                primerApellido = "Cumplido",
                segundoApellido = "Negrete",
                telefono = "2281001122",
                fechaInscripcion = fechaInscipcion,
                fechaNacimiento = fechaNacimiento,
                Direccion = new Direccion()
                {
                    calle = "Av.Xalapa",
                    codigoPostal = 91101.ToString(),
                    ciudad = "Xalapa"
                }
            };
            int resultadoEsperado = 0;
            int resultadoModificacion = socioOperaciones.EditarDatosDeSocio(numeroDeSocio, socio);
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

    }
}
