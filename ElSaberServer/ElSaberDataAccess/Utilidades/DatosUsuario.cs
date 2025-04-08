using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberDataAccess.Utilidades
{
    public class DatosUsuario
    {
        public int IdAcceso { get; set; }
        public string Correo {  get; set; }
        public string TipoDeUsuario { get; set; }
        public int FK_IdUsuario {  get; set; }
        public string Nombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Puesto { get; set; }
        public string Estado { get; set; }
    }
}
