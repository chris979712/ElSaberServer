using ElSaberDataAccess;
using ElSaberDataAccess.Operaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElSaberPruebas.Excepcion
{
    public class PruebaDeModificarExcepcion
    {
        [Fact]
        public void PruebaModificacionAccesoExcepcionExitosa()
        {
            AccesoOperaciones acceso = new AccesoOperaciones();
            string correo = "chrisvasquez985@gmail.com";
            string contrasenia = "contraseniasecreta123";
            int resultadoModificacion = acceso.ModificarContrasenia(correo, contrasenia);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificarEstadoSocioExcepcionExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 91291;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        [Fact]
        public void PruebaEditarUsuarioPorIdAccesoExcepcion()
        {
            UsuarioOperaciones usuarioOperaciones = new UsuarioOperaciones();
            int idAcceso = 1;
            int resultadoEsperado = -1;
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
                }
            };
            string nuevoCorreo = "cetis175z@gmail.com";
            int resultadoObtenido = usuarioOperaciones.EditarUsuarioPorIdAcceso(idAcceso, usuario, nuevoCorreo);
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
            int resultadoEsperado = -1;
            int resultadoModificacion = socioOperaciones.EditarDatosDeSocio(numeroDeSocio, socio);
            Assert.Equal(resultadoEsperado, resultadoModificacion);
        }

        [Fact]
        public void PruebaModificarEstadoSocioExistenteExitosa()
        {
            SocioOperaciones socioOperaciones = new SocioOperaciones();
            int numeroDeSocio = 1;
            string estadoSocio = "Desactivo";
            int resultadoModificacion = socioOperaciones.CambiarEstadoDeSocio(numeroDeSocio, estadoSocio);
            int resultadoEsperado = -1;
            Assert.Equal(resultadoModificacion, resultadoEsperado);
        }

        [Fact]
        public void PruebaModificarEstadoDeLibroExcepcionExitosa()
        {
            LibroOperaciones libroOperaciones = new LibroOperaciones();
            int resultadoEsperado = -1;
            int resultadoObtenido = libroOperaciones.CambiarEstadoDeLibro("1111111112222", "NoDisponible");
            Assert.Equal(resultadoObtenido, resultadoEsperado);
        }

        [Fact]
        public void PruebaEditarDatosDeLibroExcepcionExitosa()
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
            int resultadoEsperado = -1;
            Assert.Equal(resultadoEsperado, resultadoObtenido);
        }

    }
}