using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Servidor
{
    public class PruebaReporteInventarioLibro
    {
        [Fact]
        public void PruebaCreacionDeReporteInventarioLibroExitosa()
        {
            ElSaberProxy.ReporteInventarioLibroManejadorClient reporteInventarioLibroManejador = new ElSaberProxy.ReporteInventarioLibroManejadorClient();
            byte[] reporteObtenido = reporteInventarioLibroManejador.ObtenerReporteInventarioLibros();
            Assert.True(reporteObtenido.Length > 255);
        }
    }
}
