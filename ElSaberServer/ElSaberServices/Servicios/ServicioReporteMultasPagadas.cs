using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberServices.Contratos;
using ElSaberServices.Utilities;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : IReporteMultasPagadas
    {
        public byte[] ObtenerReporteMultasPagadasEnFechas(string fechaInicioBusqueda, string fechaFinBusqueda)
        {
            byte[] reporteGenerado = new byte[0];
            MultaOperaciones multaOperaciones = new MultaOperaciones();
            List<Multa> multas = multaOperaciones.ObtenerMultasPagadasEnDeterminadasFechas(fechaInicioBusqueda, fechaFinBusqueda);
            if (multas[0].IdMulta == -1)
            {
                reporteGenerado = new byte[255];
            }
            else if (multas[0].IdMulta != 0)
            {
                LoggerManager logger = new LoggerManager(this.GetType());
                try
                {
                    using (MemoryStream memoriaStream = new MemoryStream())
                    {
                        ServicioGeneradorDeElementosReporte generadorElementos = new ServicioGeneradorDeElementosReporte();
                        PdfWriter escritor = new PdfWriter(memoriaStream);
                        PdfDocument pdf = new PdfDocument(escritor);
                        Document documento = new Document(pdf);
                        documento.SetMargins(40, 40, 40, 40);
                        Paragraph parrafoInicial = generadorElementos.GenerarParrafoInicial();
                        Paragraph tipoDeReporte = generadorElementos.GenerarTipoDeReporte("Reporte de Multas pagadas");
                        Table tablaPrestamosPendientes = generadorElementos.GenerarTablaMultasPagadas(multas);
                        Paragraph generarParrafoFinal = generadorElementos.GenerarPieFinalDeReporte("\nEl presente reporte muestra las multas pagadas a prestamos retrasados " +
                            "en la Biblioteca El Saber, mostrando los detalles del prestamo y socio asociado con la multa pagada entre las fechas "+ fechaInicioBusqueda + " al "+fechaFinBusqueda);
                        Image imagenEncabezado = generadorElementos.AgregarImagenEncabezado(pdf);
                        documento.Add(imagenEncabezado);
                        documento.Add(parrafoInicial);
                        documento.Add(tipoDeReporte);
                        documento.Add(tablaPrestamosPendientes);
                        documento.Add(generarParrafoFinal);
                        generadorElementos.AgregarBordeAlDocumento(pdf);
                        documento.Close();
                        reporteGenerado = memoriaStream.ToArray();
                    }
                }
                catch (PdfException pdfException)
                {
                    logger.LogError(pdfException);
                    reporteGenerado = new byte[1];
                }
            }
            return reporteGenerado;
        }
    }
}
