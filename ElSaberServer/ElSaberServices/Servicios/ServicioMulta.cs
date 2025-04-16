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
    public partial class ElSaberServices : IMultaManejador
    {
        public int ActualizarEstadoMultas()
        {
            MultaOperaciones multaOperaciones= new MultaOperaciones();
            return multaOperaciones.ActualizarMultas();
        }

        public List<MultaBinding> ObtenerMultasPendientesPorNumeroSocio(int numeroSocio)
        {
            MultaOperaciones multaOperaciones = new MultaOperaciones();
            List<Multa> multasRecuperadas=multaOperaciones.RecuperarMultasPendientesPorNumeroSocio(numeroSocio);
            List<MultaBinding> multasObtenidas = new List<MultaBinding>();
            foreach (Multa multa in multasRecuperadas) 
            {
                multasObtenidas.Add(new MultaBinding 
                {
                    idMulta=multa.IdMulta,
                    FK_IdPrestamo=multa.FK_IdPrestamo,
                    Estado=multa.estado,
                    MontoTotal=multa.montoTotal,
                    PrestamoAsociado=new PrestamoBinding 
                    {
                        FK_IdLibro=multa.Prestamo.FK_IdLibro,
                    }
                });
            }
            return multasObtenidas;
        }

        public int RegistrarPagoMultaPorIdMulta(int idMulta)
        {
            MultaOperaciones multaOperaciones= new MultaOperaciones();
            return multaOperaciones.RegistrarPagoMultaPorId(idMulta);
        }
    }
}
