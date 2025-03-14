using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberServices.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : IDevolucionManejador
    {
        public int RegistrarNuevaDevolucion(DevolucionBinding devolucion)
        {
            DevolucionOperaciones devolucionOperaciones=new DevolucionOperaciones();
            Devolucion nuevaDevolucion = new Devolucion 
            {
                FK_IdPrestamo=devolucion.FK_IdPrestamo, 
                fechaDevolucion=devolucion.FechaDevolucion,
                nota=devolucion.Nota,
                estadoLibro=devolucion.EstadoLibro,
            };
            return devolucionOperaciones.RegistrarDevolucionEnLaBaseDeDatos(nuevaDevolucion);
        }
    }
}
