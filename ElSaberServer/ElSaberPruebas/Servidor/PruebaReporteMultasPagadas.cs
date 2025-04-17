using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Servidor
{
    public class PruebaReporteMultasPagadas
    {
        [Fact]
        public void PruebaCrearReporteDeLibrosMasPrestados()
        {
            ElSaberProxy.ReporteMultasPagadasClient reporteMultasPagadas = new ElSaberProxy.ReporteMultasPagadasClient();
            byte[] reporteGenerado = reporteMultasPagadas.ObtenerReporteMultasPagadasEnFechas("2025-04-11", "2025-04-16");
            string rutaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string rutaArchivo = Path.Combine(rutaDescargas, "ReporteMultasPagadas.pdf");
            File.WriteAllBytes(rutaArchivo, reporteGenerado);
            Assert.True(reporteGenerado.Length > 255);
        }
    }
}
