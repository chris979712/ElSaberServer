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

        [Fact]
        public void PruebaModificarDatosSocioExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 1;
            DateTime fechaInscipcion = DateTime.Parse("2025-04-12");
            DateTime fechaNacimiento = DateTime.Parse("2008-06-12");
            Socio socio = new Socio()
            {
                nombre = "Juan",
                primerApellido = "Cumplido",
                segundoApellido = "Negrete",
                telefono = "2281001122",
                fechaInscripcion = fechaInscipcion,
                fechaNacimiento = fechaNacimiento,
                Direccion = new Direccion()
                {
                    calle = "Av.Xalapa",
                    codigoPostal = 91101.ToString(),
                    ciudad = "Xalapa"
                }
            };
            int resultadoEsperado = 1;
            int resultadoModificacion = socioOperaciones.EditarDatosDeSocio(numeroDeSocio,socio);
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificarDatosSocioSinExistenciaExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 919191;
            DateTime fechaInscipcion = DateTime.Parse("2025-04-12");
            DateTime fechaNacimiento = DateTime.Parse("2008-06-12");
            Socio socio = new Socio()
            {
                nombre = "Juan",
                primerApellido = "Cumplido",
                segundoApellido = "Negrete",
                telefono = "2281001122",
                fechaInscripcion = fechaInscipcion,
                fechaNacimiento = fechaNacimiento,
                Direccion = new Direccion()
                {
                    calle = "Av.Xalapa",
                    codigoPostal = 91101.ToString(),
                    ciudad = "Xalapa"
                }
            };
            int resultadoEsperado = 0;
            int resultadoModificacion = socioOperaciones.EditarDatosDeSocio(numeroDeSocio, socio);
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        //Prueba Modificacion Libro
        [Fact]
        public void PruebaModificarEstadoDeLibroExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int resultadoEsperado = 1;
            int resultadoObtenido = libroOperaciones.CambiarEstadoDeLibro("9781501142970", "NoDisponible");
            Assert.Equal(resultadoObtenido, resultadoEsperado);
        }

        [Fact]
        public void PruebaModificarEstadoDeLibroInexistenteExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int resultadoEsperado = 0;
            int resultadoObtenido = libroOperaciones.CambiarEstadoDeLibro("1111111112222", "NoDisponible");
            Assert.Equal(resultadoObtenido, resultadoEsperado);
        }

        [Fact]
        public void PruebaEditarDatosDeLibroExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            Libro libroAModificar = new Libro()
            {
                titulo = "El principito",
                anioDePublicacion = "2000",
                cantidadEjemplares = 12,
                numeroDePaginas = "175",
                rutaPortada = "/ImagenesLibros/Elprincipito.jpg",
                FK_IdAutor = 1,
                FK_IdEditorial = 1,
                FK_IdGenero = 3,
            };
            int resultadoObtenido = libroOperaciones.EditarDatosLibro("9781501142970", libroAModificar);
            int resultadoEsperado = 1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        [Fact]
        public void PruebaEditarDatosDeLibroInexistenteExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            Libro libroAModificar = new Libro()
            {
                titulo = "El principito",
                anioDePublicacion = "2000",
                cantidadEjemplares = 12,
                numeroDePaginas = "175",
                rutaPortada = "/ImagenesLibros/Elprincipito.jpg",
                FK_IdAutor = 1,
                FK_IdEditorial = 2,
                FK_IdGenero = 3,
            };
            int resultadoObtenido = libroOperaciones.EditarDatosLibro("1111111111111", libroAModificar);
            int resultadoEsperado = 0;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

        //Prueba Modificacion Multa
        [Fact]
        public void PruebaRegistrarPagoMultaPorIdExitosa() 
        {
            MultaOperaciones multaOperaciones=new MultaOperaciones();
            int idMulta = 1;
            int resultadoEsperado = 1;
            int resultadoObtenido = multaOperaciones.RegistrarPagoMultaPorId(idMulta);
            Assert.Equal(resultadoEsperado,resultadoObtenido);
        }

    }
}
