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
    public partial class ElSaberServices : IUsuarioManejador
    {
        public int RegistrarUsuarioAlaBaseDeDatos(UsuarioBinding usuario, DireccionBinding direccion, AccesoBinding acceso)
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            Usuario usuarioNuevo = new Usuario()
            {
                nombre = usuario.nombre,
                primerApellido = usuario.nombre,
                segundoApellido = usuario.nombre,
                telefono = usuario.telefono,
                puesto = usuario.puesto,
            };
            Direccion direccionNueva = new Direccion()
            {
                calle = direccion.calle,
                numero = direccion.numero,
                codigoPostal = direccion.codigoPostal,
                ciudad = direccion.ciudad
            };
            Acceso accesoNuevo = new Acceso()
            {
                correo = acceso.correo,
                contrasenia = acceso.contrasenia,
                tipoDeUsuario = acceso.tipoDeUsuario
            };
            int resultadoInsercion = usuarioOperaciones.RegistrarUsuarioEnLaBaseDeDatos(usuarioNuevo, accesoNuevo, direccionNueva);
            return resultadoInsercion;
        }
    }
}
