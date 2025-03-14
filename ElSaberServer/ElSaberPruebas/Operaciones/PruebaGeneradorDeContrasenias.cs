using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ElSaberServices;
using ElSaberServices.Utilidades;
using System.Text.RegularExpressions;

namespace ElSaberPruebas.Operaciones
{
    public class PruebaGeneradorDeContrasenias
    {
        private static Regex _ContraseniaRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[^ ]{8,10}$", RegexOptions.None, TimeSpan.FromMilliseconds(1000));

        [Fact]
        public void VerificarLaCreacionDeContrasenia()
        {
            string contraseniaObtenida = GeneradorContrasenia.GenerarContrasena();
            bool resultadoValidacion = _ContraseniaRegex.IsMatch(contraseniaObtenida);
            Assert.True(resultadoValidacion);
        }
    }
}
