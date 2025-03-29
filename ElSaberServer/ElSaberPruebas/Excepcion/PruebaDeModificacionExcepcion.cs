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
    public class PruebaDeModificarExcepcion
    {
        [Fact]
        public void PruebaModificacionAccesoExcepcionExitosa()
        {
            AccesoOperaciones acceso = new AccesoOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoModificacion = acceso.ModificarContrasenia(correo, contrasenia);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificarEstadoSocioExcepcionExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 91291;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        [Fact]
        public void PruebaEditarUsuarioPorIdAccesoExcepcion()
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int idAcceso = 1;
            int resultadoEsperado = -1;
            Usuario usuario = new Usuario()
            {
                nombre = "Ivan",
                primerApellido = "Rodriguez",
                segundoApellido = "Franco",
                telefono = "2294634506",
                puesto = "Mostrador",
                Direccion = new Direccion()
                {
                    calle = "Av. principal",
                    ciudad = "Jalcomulco",
                    codigoPostal = "94000",
                }
            };
            string nuevoCorreo = "cetis175z@gmail.com";
            int resultadoObtenido = usuarioOperaciones.EditarUsuarioPorIdAcceso(idAcceso, usuario, nuevoCorreo);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}