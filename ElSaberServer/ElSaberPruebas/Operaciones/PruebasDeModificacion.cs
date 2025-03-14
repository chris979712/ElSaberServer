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

    }
}
