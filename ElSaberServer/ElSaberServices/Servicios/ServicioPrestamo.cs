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
    public partial class ElSaberServices : IPrestamoManejador
    {
        public List<PrestamoBinding> ObtenerPrestamosActivosYVencidosPorNumeroSocio(int numeroSocio)
        {
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            List<Prestamo> prestamosObtenidos = prestamoOperaciones.RecuperarPrestamosActivosYVencidosPorNumeroSocio(numeroSocio);
            List<PrestamoBinding> prestamosRecuperados = new List<PrestamoBinding>();
            foreach (Prestamo prestamoObtenido in prestamosObtenidos)
            {
                prestamosRecuperados.Add(new PrestamoBinding
                {
                    IdPrestamo=prestamoObtenido.IdPrestamo,
                    FechaPrestamo=prestamoObtenido.fechaPrestamo,
                    Estado=prestamoObtenido.estado,
                    FechaDevolucionEsperada=prestamoObtenido.fechaDevolucionEsperada,
                    Nota=prestamoObtenido.nota,
                    FK_IdLibro=prestamoObtenido.FK_IdLibro,
                    FK_IdSocio=prestamoObtenido.FK_IdSocio,
                    FK_IdUsuario=prestamoObtenido.FK_IdSocio,
                });
            }
            return prestamosRecuperados;
        }

        public int RegistrarNuevoPrestamo(PrestamoBinding prestamo)
        {
            Prestamo nuevoPrestamo = new Prestamo() 
            {
                fechaPrestamo=prestamo.FechaPrestamo,                
                fechaDevolucionEsperada=prestamo.FechaDevolucionEsperada,
                nota=prestamo.Nota,
                FK_IdLibro=prestamo.FK_IdLibro,
                FK_IdSocio=prestamo.FK_IdSocio,
                FK_IdUsuario=prestamo.FK_IdUsuario,
            };
            PrestamoOperaciones prestamoOperaciones = new PrestamoOperaciones();
            return prestamoOperaciones.RegistrarPrestamoEnLaBaseDeDatos(nuevoPrestamo);
        }

        public int ValidarPrestamosVencidosPorNumeroSocio(int numeroSocio)
        {
            PrestamoOperaciones prestamoOperaciones=new PrestamoOperaciones();
            return prestamoOperaciones.ValidarExistenciaPrestamosVencidosPorNumeroSocio(numeroSocio);
        }
    }
}
