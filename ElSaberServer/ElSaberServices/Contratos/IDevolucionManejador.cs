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
    public interface IDevolucionManejador
    {
        [OperationContract]
        int RegistrarNuevaDevolucion(DevolucionBinding devolucion);
    }

    [DataContract]
    public class DevolucionBinding 
    {
        [DataMember]
        public int IdDevolucion { set; get; }

        [DataMember]
        public int FK_IdPrestamo { set; get; }

        [DataMember]
        public DateTime FechaDevolucion { set; get; }

        [DataMember]
        public string Nota { set; get; }

        [DataMember]
        public string EstadoLibro { set; get; }
    }
}
