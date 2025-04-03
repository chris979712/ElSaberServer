using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using ElSaberServices.Contratos;
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
    public partial class ElSaberServices : IReporteSocioConPrestamosPendientes
    {
        public byte[] GenerarReporteSocioConPrestamosPendientes()
        {
            byte[] reporteGenerado = new byte[0];
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            List<SocioPrestamoPendiente> prestamosPendientes = prestamoOperaciones.ObtenerPrestamosPendientes();
            if (prestamosPendientes[0].idPrestamo == -255)
            {
                reporteGenerado = new byte[255];
            }
            else if (prestamosPendientes[0].idPrestamo != 0)
            {
                LoggerManager logger = new LoggerManager(this.GetType());
                try
                {
                    using(MemoryStream memoriaStream = new MemoryStream())
                    {
                        ServicioGeneradorDeElementosReporte servicio = new ServicioGeneradorDeElementosReporte();
                        ServicioGeneradorDeElementosReporte generadorElementos = new ServicioGeneradorDeElementosReporte();
                        PdfWriter escritor = new PdfWriter(memoriaStream);
                        PdfDocument pdf = new PdfDocument(escritor);
                        Document documento = new Document(pdf);
                        documento.SetMargins(40, 40, 40, 40);
                        Paragraph parrafoInicial = generadorElementos.GenerarParrafoInicial();
                        Paragraph tipoDeReporte = generadorElementos.GenerarTipoDeReporte("Reporte de Préstamos pendientes");
                        Table tablaPrestamosPendientes = generadorElementos.GenerarTablaSocioConPrestamoPendiente(prestamosPendientes);
                        Paragraph generarParrafoFinal = generadorElementos.GenerarPieFinalDeReporte("\nEl presente reporte muestra los préstamos pendientes " +
                            "en la Biblioteca El Saber, mostrando los detalles del libro y socio asociado con el préstamo pendiente. ");
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
