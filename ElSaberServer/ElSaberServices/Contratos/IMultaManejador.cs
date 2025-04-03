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
    public interface IMultaManejador
    {
        [OperationContract]
        int RegistrarPagoMultaPorIdMulta(int idMulta);

        [OperationContract]
        List<MultaBinding> ObtenerMultasPendientesPorNumeroSocio(int numeroSocio);

        [OperationContract]
        int ActualizarEstadoMultas();
    }

    [DataContract]
    public class MultaBinding 
    {
        [DataMember]
        public int idMulta;

        [DataMember]
        public int FK_IdPrestamo;

        [DataMember]
        public double MontoTotal;

        [DataMember]
        public string Estado;
    }
}
