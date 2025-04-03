using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Servidor
{
    public class PruebaReporteSocioConPrestamosPendientes
    {
        [Fact]
        public void PruebaCrearReporteDeLibrosMasPrestados()
        {
            ElSaberProxy.ReporteSocioConPrestamosPendientesClient reporteSocioConPrestamosPendientes = new ElSaberProxy.ReporteSocioConPrestamosPendientesClient();
            byte[] reporteGenerado = reporteSocioConPrestamosPendientes.GenerarReporteSocioConPrestamosPendientes();
            string rutaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            string rutaArchivo = Path.Combine(rutaDescargas, "ReportePrestamosPendientes.pdf");
            File.WriteAllBytes(rutaArchivo, reporteGenerado);
            Assert.True(reporteGenerado.Length > 255);
        }
    }
}
