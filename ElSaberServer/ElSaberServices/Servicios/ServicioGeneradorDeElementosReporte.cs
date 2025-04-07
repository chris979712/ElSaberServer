using ElSaberDataAccess;
using ElSaberDataAccess.Utilidades;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.IO;

namespace ElSaberServices.Servicios
{
    public class ServicioGeneradorDeElementosReporte
    {

        public Paragraph GenerarParrafoInicial()
        {
            var timesNewRoman = PdfFontFactory.CreateFont(StandardFonts.TIMES_ITALIC);
            Paragraph parrafo = new Paragraph();
            parrafo.SetTextAlignment(TextAlignment.RIGHT);
            parrafo.SetFont(timesNewRoman);
            parrafo.SetFontSize(14);
            Text textoFecha = new Text(DateTime.Now.ToString("yyyy-MM-dd"));
            Text textoBiblioteca = new Text("\nBiblioteca El saber.\n");
            Text lugar = new Text("Xalapa,Ver.\n\n");
            parrafo.Add(textoFecha);
            parrafo.Add(textoBiblioteca);
            parrafo.Add(lugar);
            return parrafo;
        }

        public Paragraph GenerarTipoDeReporte(string tipoDeReporte)
        {
            var negritaTimes = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
            Paragraph parrafo = new Paragraph();
            parrafo.SetTextAlignment(TextAlignment.CENTER);
            parrafo.SetFont(negritaTimes);
            parrafo.SetFontSize(20);
            Text textoTipoDeReporte = new Text(tipoDeReporte);
            parrafo.Add(textoTipoDeReporte);
            return parrafo;
        }

        public Table GenerarTablaReporteInventarioLibros(List<InventarioLibro> inventarioDeLibros)
        {
            Table tablaInventarioLibros = new Table(5, true);
            tablaInventarioLibros.AddCell(CrearCeldaEncabezado("Título del libro"));
            tablaInventarioLibros.AddCell(CrearCeldaEncabezado("ISBN"));
            tablaInventarioLibros.AddCell(CrearCeldaEncabezado("Total de ejemplares"));
            tablaInventarioLibros.AddCell(CrearCeldaEncabezado("Total disponibles"));
            tablaInventarioLibros.AddCell(CrearCeldaEncabezado("Total No disponibles"));
            foreach (var libro in inventarioDeLibros)
            {
                tablaInventarioLibros.AddCell(CrearCeldaElementos(libro.tituloLibro));
                tablaInventarioLibros.AddCell(CrearCeldaElementos(libro.ISBN));
                tablaInventarioLibros.AddCell(CrearCeldaElementos(libro.cantidadTotal.ToString()));
                tablaInventarioLibros.AddCell(CrearCeldaElementos(libro.cantidadDisponible.ToString()));
                tablaInventarioLibros.AddCell(CrearCeldaElementos(libro.cantidadNoDisponible.ToString()));
            }

            return tablaInventarioLibros;
        }

        public Table GenerarTablaLibrosMasPrestados(List<LibroMasPrestado> libroMasPrestados)
        {
            Table tablaLibrosMasPrestados = new Table(5, true);
            tablaLibrosMasPrestados.AddCell(CrearCeldaEncabezado("Título del libro"));
            tablaLibrosMasPrestados.AddCell(CrearCeldaEncabezado("ISBN"));
            tablaLibrosMasPrestados.AddCell(CrearCeldaEncabezado("Autor"));
            tablaLibrosMasPrestados.AddCell(CrearCeldaEncabezado("Genero"));
            tablaLibrosMasPrestados.AddCell(CrearCeldaEncabezado("Total ejemplares"));
            foreach(var libro in  libroMasPrestados)
            {
                tablaLibrosMasPrestados.AddCell(CrearCeldaElementos(libro.titulo));
                tablaLibrosMasPrestados.AddCell(CrearCeldaElementos(libro.isbn));
                tablaLibrosMasPrestados.AddCell(CrearCeldaElementos(libro.autor));
                tablaLibrosMasPrestados.AddCell(CrearCeldaElementos(libro.genero));
                tablaLibrosMasPrestados.AddCell(CrearCeldaElementos(libro.cantidadDeEjemplares));
            }
            return tablaLibrosMasPrestados;
        }

        public Table GenerarTablaSocioConPrestamoPendiente(List<SocioPrestamoPendiente> sociosPrestamoPendientes)
        {
            Table tablaSocioPrestamoPendiente = new Table(8, true);
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("No.Socio"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("Nombre socio"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("Teléfono"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("Id Prestamo"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("ISBN"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("Titulo"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("Fecha inicio prestamo"));
            tablaSocioPrestamoPendiente.AddCell(CrearCeldaEncabezado("Fecha devolución esperada"));
            foreach (var socio in sociosPrestamoPendientes)
            {
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.numeroSocio.ToString()));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.nombreSocio));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.telefonoSocio));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.idPrestamo.ToString()));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.isbnLibro));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.tituloLibro));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.fechaPrestamo));
                tablaSocioPrestamoPendiente.AddCell(CrearCeldaElementos(socio.fechaDevolucionEsperada));
            }
            return tablaSocioPrestamoPendiente;
        }

        private Cell CrearCeldaEncabezado(string texto)
        {
            var negrita = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLDITALIC);
            Color colorFondoEncabezado = ColorConstants.LIGHT_GRAY;
            return new Cell()
                   .Add(new Paragraph(texto).SetFont(negrita))
                   .SetBackgroundColor(colorFondoEncabezado)
                   .SetTextAlignment(TextAlignment.CENTER);
        }

        private Cell CrearCeldaElementos(string texto)
        {
            var timesNewRoman = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            return new Cell()
                    .Add(new Paragraph(texto).SetFont(timesNewRoman))
                    .SetTextAlignment(TextAlignment.CENTER);
        }

        public Paragraph GenerarPieFinalDeReporte(string texto)
        {
            var timesNewRoman = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);
            Paragraph parrafo = new Paragraph(texto);
            parrafo.SetFontSize(14);
            parrafo.SetFont(timesNewRoman);
            parrafo.SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED);
            return parrafo;
        }

        public Image AgregarImagenEncabezado(PdfDocument pdf)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string serverPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(baseDirectory, "../../../"));
            string direccionImagen = System.IO.Path.Combine(serverPath, "ElSaberServices/Utilidades/Buho.jpg");
            ImageData imagenData = ImageDataFactory.Create(direccionImagen);
            Image imagen = new Image(imagenData);
            if (pdf.GetNumberOfPages() == 0)
            {
                pdf.AddNewPage();
            }
            PdfPage pagina = pdf.GetFirstPage();
            Rectangle tamanoPagina = pagina.GetPageSize();
            float x = tamanoPagina.GetLeft() + 40; 
            float y = tamanoPagina.GetTop() - imagen.GetImageHeight() + 400; 
            imagen.SetFixedPosition(x, y);
            imagen.ScaleToFit(100, 75);
            return imagen;
        }

        public void AgregarBordeAlDocumento(PdfDocument pdf, float grosorBorde = 2, float margenBorde = 10)
        {
            if (pdf.GetNumberOfPages() > 0)
            {
                PdfPage primeraPagina = pdf.GetFirstPage();
                PdfCanvas pdfCanvas = new PdfCanvas(primeraPagina);
                Rectangle tamanoPagina = primeraPagina.GetPageSize();
                pdfCanvas
                    .SetStrokeColor(ColorConstants.BLACK) 
                    .SetLineWidth(grosorBorde) 
                    .Rectangle(
                        margenBorde, margenBorde,
                        tamanoPagina.GetWidth() - 2 * margenBorde,
                        tamanoPagina.GetHeight() - 2 * margenBorde)
                    .Stroke();
            }
        }

    }
}
