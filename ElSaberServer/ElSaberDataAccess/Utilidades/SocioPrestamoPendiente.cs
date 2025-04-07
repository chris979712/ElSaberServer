using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberDataAccess.Utilidades
{
    public class SocioPrestamoPendiente
    {
        public int numeroSocio {  get; set; }

        public string nombreSocio {  get; set; }

        public string telefonoSocio { get; set; }

        public int idPrestamo { get; set; }

        public string isbnLibro { get; set; }

        public string tituloLibro { get; set; }

        public string fechaPrestamo { get; set; }

        public string fechaDevolucionEsperada { get; set; }
    }
}
