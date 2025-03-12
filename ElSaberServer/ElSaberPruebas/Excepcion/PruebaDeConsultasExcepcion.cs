using ElSaberDataAccess.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Excepcion
{
    public class PruebaDeConsultasExcepcion
    {
        [Fact]
        public void VerificarExistenciaDeCredencialesPrueba()
        {
            UsuarioOperaciones usuarioOperacion = new UsuarioOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoObtenido = usuarioOperacion.VerificarExistenciaDeUsuario(correo, contrasenia);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void VerificarExistenciaDeCredencialesSinExistirPrueba()
        {
            UsuarioOperaciones usuarioOperacion = new UsuarioOperaciones();
            string correo = "chrisvasquez999@gmail.com";
            string contrasenia = "123456Contrasenia";
            int resultadoObtenido = usuarioOperacion.VerificarExistenciaDeUsuario(correo, contrasenia);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}
