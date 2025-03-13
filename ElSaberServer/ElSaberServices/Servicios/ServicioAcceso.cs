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
        public int ModificarContrasenia(string correo, string contrasenia)
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            int resultadoModificacion = accesoOperaciones.ModificarContrasenia(correo, contrasenia);
            return resultadoModificacion;
        }

        public int VerificarCorreoExistente(string correo)
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            int resultadoVerificacion = accesoOperaciones.VerificarCorreoExistente(correo);
            return resultadoVerificacion;
        }

        public int VerificarCredenciales(string correo, string telefono)
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            int resultadoVerificacion = accesoOperaciones.VerificarCredenciales(correo, telefono);
            return resultadoVerificacion;
        }
    }
}
