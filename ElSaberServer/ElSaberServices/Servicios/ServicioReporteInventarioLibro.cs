using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using ElSaberDataAccess.Utilities;
using ElSaberServices.Contratos;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : IReporteInventarioLibroManejador
    {
        public byte[] ObtenerReporteInventarioLibros()
        {
            byte[] reporteInventario = new byte[0];
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<InventarioLibro> inventarioLibros = libroOperaciones.ObtenerInventarioLibros();
            if (inventarioLibros[0].cantidadTotal == -1)
            {
                reporteInventario = new byte[255];
            }
            else if (inventarioLibros[0].cantidadTotal > 0)
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
                        Paragraph tipoDeReporte = generadorElementos.GenerarTipoDeReporte("Reporte de inventario de libros");
                        Table tablaInventario = generadorElementos.GenerarTablaReporteInventarioLibros(inventarioLibros);
                        Paragraph generarParrafoFinal = generadorElementos.GenerarPieFinalDeReporte("\nEl presente reporte muestra el inventario " +
                            "general de los libros con los que cuenta la Biblioteca El Saber a dia " + DateTime.Now.ToString("dd") + " de " + DateTime.Now.ToString("MM") + " del " + DateTime.Now.ToString("yyyy"));
                        Image imagenEncabezado = generadorElementos.AgregarImagenEncabezado(pdf);
                        documento.Add(imagenEncabezado);
                        documento.Add(parrafoInicial);
                        documento.Add(tipoDeReporte);
                        documento.Add(tablaInventario);
                        documento.Add(generarParrafoFinal);
                        generadorElementos.AgregarBordeAlDocumento(pdf);
                        documento.Close();
                        reporteInventario = memoriaStream.ToArray();
                    }
                }
                catch(PdfException pdfException)
                {
                    logger.LogError(pdfException);
                    reporteInventario = new byte[1];
                }   
            }
            return reporteInventario;
        }
    }
}
