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
    }
}