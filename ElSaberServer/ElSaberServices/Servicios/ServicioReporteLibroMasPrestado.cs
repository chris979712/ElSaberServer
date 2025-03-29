using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using ElSaberServices.Contratos;
using ElSaberServices.Utilities;
using iText.Kernel.Exceptions;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : IReporteLibroMasPrestadoManejador
    {
        public byte[] ObtenerReporteLibrosMasPrestado(string fechaInicioBusqueda, string fechaFinBusqueda)
        {
            byte[] ReporteLibrosMasPrestados = new byte[0];
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            List<LibroMasPrestado> librosMasPrestados = libroOperaciones.ObtenerLibrosMasPrestadosPorFecha(fechaInicioBusqueda, fechaFinBusqueda);
            if (librosMasPrestados[0].cantidadDeEjemplares.Equals("-1"))
            {
                ReporteLibrosMasPrestados = new byte[255];
            }
            else if (!librosMasPrestados[0].cantidadDeEjemplares.Equals("0"))
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
                        Paragraph tipoDeReporte = generadorElementos.GenerarTipoDeReporte("Reporte de Libros más prestados");
                        Table tablaInventario = generadorElementos.GenerarTablaLibrosMasPrestados(librosMasPrestados);
                        Paragraph generarParrafoFinal = generadorElementos.GenerarPieFinalDeReporte("\nEl presente reporte muestra los libros más prestados " +
                            "en la Biblioteca El Saber desde entre las fechas "+fechaInicioBusqueda+" - "+fechaFinBusqueda);
                        Image imagenEncabezado = generadorElementos.AgregarImagenEncabezado(pdf);
                        documento.Add(imagenEncabezado);
                        documento.Add(parrafoInicial);
                        documento.Add(tipoDeReporte);
                        documento.Add(tablaInventario);
                        documento.Add(generarParrafoFinal);
                        generadorElementos.AgregarBordeAlDocumento(pdf);
                        documento.Close();
                        ReporteLibrosMasPrestados = memoriaStream.ToArray();
                    }
                }
                catch (PdfException pdfException)
                {
                    logger.LogError(pdfException);
                ReporteLibrosMasPrestados = new byte[1];
                }
            }
            return ReporteLibrosMasPrestados;
        }
    }
}
