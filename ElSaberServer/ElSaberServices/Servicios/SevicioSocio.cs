﻿using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using ElSaberServices.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : ISocioManejador
    {
        public SocioBinding ConsultarSocioPorNumeroDeSocio(int numeroDeSocio)
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            Socio socioObtenido = socioOperaciones.ObtenerSocioPorNumeroDeSocio(numeroDeSocio);
            SocioBinding socioRecibido = new SocioBinding()
            {
                numeroDeSocio = socioObtenido.numeroDeSocio,
                nombre = socioObtenido.nombre,
                primerApellido = socioObtenido.primerApellido,
                segundoApellido = socioObtenido.segundoApellido,
                telefono = socioObtenido.telefono,
                fechaDeInscripcion = socioObtenido.fechaInscripcion,
                estado = socioObtenido.estado,
                direccion = new DireccionBinding()
                {
                    IdDireccion = socioObtenido.Direccion.IdDireccion,
                    calle = socioObtenido.Direccion.calle,
                    ciudad = socioObtenido.Direccion.ciudad,
                    codigoPostal = socioObtenido.Direccion.codigoPostal,
                    numero = socioObtenido.Direccion.numero
                }
            };
            return socioRecibido;
        }

        public List<SocioBinding> ConsultarSociosPorNombre(string nombre)
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            List<Socio> sociosObtenidos = socioOperaciones.ObtenerSociosPorNombre(nombre);
            List<SocioBinding> sociosRecibidos = new List<SocioBinding>();
            foreach(Socio socioObtenido in sociosObtenidos)
            {
                sociosRecibidos.Add(new SocioBinding() {
                    numeroDeSocio = socioObtenido.numeroDeSocio,
                    nombre = socioObtenido.nombre,
                    primerApellido = socioObtenido.primerApellido,
                    segundoApellido = socioObtenido.segundoApellido,
                    telefono = socioObtenido.telefono,
                    fechaDeInscripcion = socioObtenido.fechaInscripcion,
                    estado = socioObtenido.estado,
                    direccion = new DireccionBinding()
                    {
                        IdDireccion = socioObtenido.Direccion.IdDireccion,
                        calle = socioObtenido.Direccion.calle,
                        ciudad = socioObtenido.Direccion.ciudad,
                        codigoPostal = socioObtenido.Direccion.codigoPostal,
                        numero = socioObtenido.Direccion.numero
                    }
                });
            }
            return sociosRecibidos;
        }

        public int RegistrarSocioEnBaseDeDatos(SocioBinding socio)
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            Socio socioNuevo = new Socio()
            {
                nombre = socio.nombre,
                primerApellido = socio.primerApellido,
                segundoApellido = socio.segundoApellido,
                telefono = socio.telefono,
                fechaInscripcion = socio.fechaDeInscripcion
            };
            Direccion direccionNueva = new Direccion()
            {
                calle = socio.direccion.calle,
                numero = socio.direccion.numero,
                codigoPostal = socio.direccion.codigoPostal,
                ciudad = socio.direccion.ciudad
            };
            int resultadoInsercion = socioOperaciones.RegistrarNuevoSocio(socioNuevo, direccionNueva); 
            return resultadoInsercion;
        }

        public int VerificarExistenciaDeSocioEnBaseDeDatos(string telefono)
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int resultadoVerificacion = socioOperaciones.VerificarSocioRegistradoEnLaBaseDeDatos(telefono);
            return resultadoVerificacion;
        }
    }
}
