using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using System;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace ElSaberServerTest.Operaciones
{
	
	public class PruebasDeModificacion
	{
        /**
         * Prueba Modificacion Acceso
         */
        [Fact]
        public void PruebaModificacionAccesoExitosa()
        {
            AccesoOperaciones acceso = new AccesoOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoModificacion = acceso.ModificarContrasenia(correo,contrasenia);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificacionAccesoInexistenteExitosa()
        {
            AccesoOperaciones acceso = new AccesoOperaciones();
            string correo = "chris9999@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoModificacion = acceso.ModificarContrasenia(correo, contrasenia);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        /**
         * Prueba Modificacion Socio
         */
        [Fact]
        public void PruebaModificarEstadoSocioExistenteExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 1;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        [Fact]
        public void PruebaModificarEstadoSocioNoExistenteExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 91291;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        /**
         * Prueba Modificacion Prestamo
         */
        [Fact]
        public void PruebaEditarPrestamoPorIdPrestamoExitosa() 
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            int idPrestamo = 1;
            string nuevaNota = "Nueva nota de edicion";
            DateTime nuevaFechaDevolucion=DateTime.Now;
            int resultadoEsperado = 1;
            int resultadoObtenido = prestamoOperaciones.EditarPrestamoPorIdPrestamo(idPrestamo,nuevaNota,nuevaFechaDevolucion);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        /**
         * Prueba Modificacion Usuario
         */
        [Fact]
        public void PruebaDesactivarUsuarioPorIdExitosa() 
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int idUsuario = 1;
            int resultadoEsperado = 1;
            int resultadoObtenido=usuarioOperaciones.DesactivarUsuarioPorId(idUsuario);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaEditarUsuarioPorIdAccesoExitosa() 
        {
            UsuarioOperaciones usuarioOperaciones=new UsuarioOperaciones();
            int idAcceso = 1;
            int resultadoEsperado = 1;
            Usuario usuario = new Usuario()
            {
                nombre = "Ivan",
                primerApellido = "Rodriguez",
                segundoApellido = "Franco",
                telefono = "2294634506",
                puesto = "Mostrador",
                Direccion = new Direccion()
                {
                    calle = "Av. principal",
                    ciudad = "Jalcomulco",
                    codigoPostal = "94000",
                    numero = "78"
                }
            };            
            string nuevoCorreo = "cetis175z@gmail.com";            
            int resultadoObtenido=usuarioOperaciones.EditarUsuarioPorIdAcceso(idAcceso, usuario, nuevoCorreo);
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }
    }
}
