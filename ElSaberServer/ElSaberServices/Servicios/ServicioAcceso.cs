using ElSaberServices.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : IAccesoManejador
    {
        public int VerificarCredenciales(string correo, string telefono)
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            int resultadoVerificacion = accesoOperaciones.VerificarCredenciales(correo, telefono);
            return resultadoVerificacion;
        }
    }
}
