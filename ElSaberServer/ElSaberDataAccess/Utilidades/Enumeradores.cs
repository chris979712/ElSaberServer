using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberDataAccess.Utilidades
{
    public static class Enumeradores
    {
        public enum EnumeradorEstadoUsuario
        {
            Activo,
            Desactivado
        }

        public enum EnumeradoEstadoPrestamo 
        {
            Activo,
            Vencido,
            Devuelto,
        }
    }
}
