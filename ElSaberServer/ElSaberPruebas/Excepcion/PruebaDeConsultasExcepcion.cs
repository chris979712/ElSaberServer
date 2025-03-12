using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberPruebas.Utilities;
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


    }
}
