using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Contratos
{
    [ServiceContract]
    public interface IPrestamoManejador
    {
        [OperationContract]
        int RegistrarNuevoPrestamo(PrestamoBinding prestamo);

        [OperationContract]
        List<PrestamoBinding> ObtenerPrestamosActivosYVencidosPorNumeroSocio(int numeroSocio);

        [OperationContract]
        int ValidarPrestamosVencidosPorNumeroSocio(int numeroSocio);

        [OperationContract]
        List<PrestamoBinding> RecuperarTodosPrestamosPorNumeroSocio(int numeroSocio);

        [OperationContract]
        List<PrestamoBinding> RecuperarPrestamosActivosPorNumeroSocio(int numeroSocio);

        [OperationContract]
        List<PrestamoBinding> ObtenerPrestamosActivosPorISBN(string isbn);

        [OperationContract]
        List<PrestamoBinding> ObtenerPrestamosActivosPorFechaInicio(DateTime fechaInicio);

        [OperationContract]
        int EditarPrestamoPorId(int idPrestamo, string nota, DateTime fechaDevolucionEsperada);
    }

    [DataContract]
    public class PrestamoBinding 
    {
        [DataMember]
        public int IdPrestamo { get; set; }

        [DataMember]
        public DateTime FechaPrestamo { get; set; }

        [DataMember]
        public string Estado { get; set; }

        [DataMember]
        public DateTime FechaDevolucionEsperada { get; set; }

        [DataMember]
        public string Nota {  get; set; }

        [DataMember]
        public int FK_IdLibro {  get; set; }

        [DataMember]
        public int FK_IdUsuario { get; set; }

        [DataMember]
        public int FK_IdSocio {  get; set; }

        [DataMember]
        public string TituloLibro { get; set; }
    }
}
