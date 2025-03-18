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

        public override bool Equals(object obj)
        {
            if (obj is DatosUsuario otro)
            {
                return IdAcceso == otro.IdAcceso && Correo == otro.Correo &&
                    TipoDeUsuario == otro.TipoDeUsuario && FK_IdUsuario == otro.FK_IdUsuario &&
                    Nombre == otro.Nombre && PrimerApellido == otro.PrimerApellido &&
                    SegundoApellido == otro.SegundoApellido && Puesto == otro.Puesto;
            }
            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + IdAcceso.GetHashCode();
                hash = hash * 23 + (Correo?.GetHashCode() ?? 0);
                hash = hash * 23 + (TipoDeUsuario?.GetHashCode() ?? 0);
                hash = hash * 23 + FK_IdUsuario.GetHashCode();
                hash = hash * 23 + (Nombre?.GetHashCode() ?? 0);
                hash = hash * 23 + (PrimerApellido?.GetHashCode() ?? 0);
                hash = hash * 23 + (SegundoApellido?.GetHashCode() ?? 0);
                hash = hash * 23 + (Puesto?.GetHashCode() ?? 0);
                return hash;
            }
        }
    }
}
