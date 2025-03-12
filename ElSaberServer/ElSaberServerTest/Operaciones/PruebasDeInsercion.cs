﻿using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;
using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;

namespace ElSaberServerTest.Operaciones
{

    public class PruebasDeInsercion
    {
        [Fact]
        public void PruebaRegistrarUsuarioEnLaBaseDeDatos()
        {
            Direccion direccion = new Direccion()
            {
                calle = "Juarez",
                numero = "57",
                codigoPostal = "91000",
                ciudad = "Xalapa"
            };

            Usuario usuario = new Usuario()
            {
                nombre = "Christopher",
                primerApellido = "Vasquez",
                segundoApellido = "Zapata",
                telefono = "2281024672",
                puesto = "Administrado"
            };

            Acceso acceso = new Acceso()
            {
                correo = "chrisvasquez985@gmail.com",
                contrasenia = "contraseniasecreta123",
                tipoDeUsuario = "Administrado"
            };
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int resultadoObtenido = usuarioOperaciones.RegistrarUsuarioEnLaBaseDeDatos(usuario, acceso, direccion);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}
