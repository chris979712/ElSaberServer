﻿using ElSaberServices.Contratos;
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
        public int DesactivarUsuarioPorIdUsuario(int idUsuario)
        {
            UsuarioOperaciones usuarioOperaciones= new UsuarioOperaciones();
            return usuarioOperaciones.DesactivarUsuarioPorId(idUsuario);
        }

        public int EditarInformacionUsuarioPorIdAcceso(int idAcceso, UsuarioBinding usuario, string correo)
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            Usuario usuarioNuevo = new Usuario()
            {
                nombre = usuario.nombre,
                primerApellido = usuario.primerApellido,
                segundoApellido = usuario.segundoApellido,
                telefono = usuario.telefono,
                puesto = usuario.puesto,
                Direccion =new Direccion() 
                {
                    ciudad=usuario.direccion.ciudad,
                    codigoPostal=usuario.direccion.codigoPostal,
                    calle=usuario.direccion.calle,
                    numero=usuario.direccion.numero,
                }
            };
            return usuarioOperaciones.EditarUsuarioPorIdAcceso(idAcceso, usuarioNuevo, correo);            
        }

        public int RegistrarUsuarioAlaBaseDeDatos(UsuarioBinding usuario, DireccionBinding direccion, AccesoBinding acceso)
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            Usuario usuarioNuevo = new Usuario()
            {
                nombre = usuario.nombre,
                primerApellido = usuario.primerApellido,
                segundoApellido = usuario.segundoApellido,
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

        public int VerificarExistenciaDeUsuario(string correo, string telefonp)
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int resultadoVerificacion = VerificarCredenciales(correo, telefonp);
            return resultadoVerificacion;
        }
    }
}
