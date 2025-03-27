using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Servidor
{
    public class PruebaReporteLibroMasPrestado
    {
        [Fact]
        public void PruebaCrearReporteDeLibrosMasPrestados()
        {
            ElSaberProxy.ReporteLibroMasPrestadoManejadorClient reporteLibroMasPrestadoManejador = new ElSaberProxy.ReporteLibroMasPrestadoManejadorClient();
            byte[] reporteObtenido = reporteLibroMasPrestadoManejador.ObtenerReporteLibrosMasPrestado("2025-03-26", "2025-03-28");
            Assert.True(reporteObtenido.Length > 255);
        }
    }
}
