using ElSaberDataAccess.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Servidor
{
    public class PruebasRestablecimientoCuenta
    {
        [Fact]
        public void PruebaEnviarCorreoRestablecimientoExitosa()
        {
            ElSaberProxy.RestablecimientoCuentaManejadorClient restablecimientoCuentaManejadorClient = new ElSaberProxy.RestablecimientoCuentaManejadorClient();
            string correo = "chrisvasquez985@gmail.com";
            int resultadoObtenido = restablecimientoCuentaManejadorClient.CorreoDeRestablecimientoDeContrasenia(correo);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaVerificarCodigoInvalidoExitosa()
        {
            ElSaberProxy.RestablecimientoCuentaManejadorClient restablecimientoCuentaManejadorClient = new ElSaberProxy.RestablecimientoCuentaManejadorClient();
            string correo = "chrisvasquez985@gmail.com";
            string codigo = "000000";
            bool resultadoVerificacion = restablecimientoCuentaManejadorClient.VerificarCodigoDeVerificacion(correo, codigo);
            Assert.True(resultadoVerificacion);
        }

        [Fact]
        public void PruebaVerificarCodigoExitosa()
        {
            ElSaberProxy.RestablecimientoCuentaManejadorClient restablecimientoCuentaManejadorClient = new ElSaberProxy.RestablecimientoCuentaManejadorClient();
            string correo = "chrisvasquez985@gmail.com";
            string codigo = "799379";
            bool resultadoVerificacion = restablecimientoCuentaManejadorClient.VerificarCodigoDeVerificacion(correo, codigo);
            Assert.True(resultadoVerificacion);
        }

        [Fact]
        public void PruebaRestablecerContraseniaExitosa()
        {
            ElSaberProxy.RestablecimientoCuentaManejadorClient restablecimientoCuentaManejadorClient = new ElSaberProxy.RestablecimientoCuentaManejadorClient();
            string correo = "chrisvasquez985@gmail.com";
            int resultadoEnvioDeCorreo = restablecimientoCuentaManejadorClient.RestablecimientoDeContrasenia(correo);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoEnvioDeCorreo);
        }
    }
}
