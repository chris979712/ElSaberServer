using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
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
    }
}
