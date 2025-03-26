using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberDataAccess.Utilidades
{
    public class InventarioLibro
    {
        public string tituloLibro { get; set; }

        public string ISBN { get; set; }

        public int cantidadTotal { get; set; };

        public int cantidadDisponible { get; set; };

        public int cantidadNoDisponible { get; set; };

    }
}
