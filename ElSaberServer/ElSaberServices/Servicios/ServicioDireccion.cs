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
    public partial class ElSaberServices : IDireccionManejador
    {
        public int RegistrarDireccion(DireccionBinding direccion)
        {
            DireccionOperaciones direccionOperaciones = new DireccionOperaciones();
            Direccion direccionNueva = new Direccion()
            {
                calle = direccion.calle,
                numero = direccion.numero,
                codigoPostal = direccion.codigoPostal,
                ciudad = direccion.ciudad
            };
            int resultadoInsercion = direccionOperaciones.AgregarNuevaDireccion(direccionNueva);
            return resultadoInsercion;
        }
    }
}
