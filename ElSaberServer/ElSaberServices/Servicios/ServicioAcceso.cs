using ElSaberServices.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilidades;
using System.Collections.Concurrent;
using ElSaberDataAccess.Utilities;
using System.ServiceModel;

namespace ElSaberServices.Servicios
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
                ConcurrencyMode = ConcurrencyMode.Multiple)]
    public partial class ElSaberServices : IAccesoManejador
    {
        private readonly ConcurrentDictionary<string, AccesoBinding> usuariosActivos = new ConcurrentDictionary<string, AccesoBinding>();
        private readonly object bloqueoInicioSesion = new object();
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

        public AccesoBinding IniciarSesion(string correo, string contrasenia)
        {
            AccesoOperaciones accesoOperaciones= new AccesoOperaciones();
            AccesoBinding accesoBinding = new AccesoBinding();

            lock (bloqueoInicioSesion)
            {
                if (usuariosActivos.ContainsKey(correo))
                {
                    accesoBinding.IdAcceso = Constantes.ErrorSesionActiva;
                    return accesoBinding;
                }

                DatosUsuario usuario = accesoOperaciones.IniciarSesion(correo, contrasenia);
                accesoBinding = new AccesoBinding()
                {
                    IdAcceso = usuario.IdAcceso,
                    correo = usuario.Correo,
                    tipoDeUsuario = usuario.TipoDeUsuario,
                    IdUsuario = new UsuarioBinding
                    {
                        IdUsuario = usuario.FK_IdUsuario,
                        nombre = usuario.Nombre,
                        primerApellido = usuario.PrimerApellido,
                        segundoApellido = usuario.SegundoApellido,
                        puesto = usuario.Puesto,
                    },
                };

                if (accesoBinding != null)
                {
                    usuariosActivos.AddOrUpdate(correo, accesoBinding, (llave, valorExistente) => accesoBinding);
                }
            }
           
            return accesoBinding;
        }

        public int CerrarSesion(string correo)
        {
            int resultado = Constantes.ErrorEnLaOperacion;

            if (usuariosActivos.TryRemove(correo, out _))
            {
                resultado = Constantes.OperacionExitosa;
            }

            return resultado;
        }

        public List<AccesoBinding> ObtenerUsuarios()
        {
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            List<Acceso> accesosRecuperados = accesoOperaciones.ObtenerUsuarios();
            List<AccesoBinding> accesosObtenidos = new List<AccesoBinding>();

            foreach (Acceso accesoRecuperado in  accesosRecuperados)
            {
                DireccionBinding direccionBinding = new DireccionBinding()
                {
                    calle = accesoRecuperado.Usuario.Direccion.calle,
                    ciudad = accesoRecuperado.Usuario.Direccion.ciudad,
                    codigoPostal = accesoRecuperado.Usuario.Direccion.codigoPostal,
                    numero = accesoRecuperado.Usuario.Direccion.numero
                };
                UsuarioBinding usuarioBinding = new UsuarioBinding()
                {
                    nombre = accesoRecuperado.Usuario.nombre,
                    primerApellido = accesoRecuperado.Usuario.primerApellido,
                    segundoApellido = accesoRecuperado.Usuario.segundoApellido,
                    telefono = accesoRecuperado.Usuario.telefono,
                    puesto = accesoRecuperado.Usuario.puesto,
                    direccion = direccionBinding
                };
                AccesoBinding accesoBinding = new AccesoBinding()
                {
                    IdAcceso = accesoRecuperado.IdAcceso,
                    correo = accesoRecuperado.correo,
                    tipoDeUsuario = accesoRecuperado.tipoDeUsuario,
                    IdUsuario = usuarioBinding
                };
                accesosObtenidos.Add(accesoBinding);
            }
            return accesosObtenidos;
        }
    }
}
