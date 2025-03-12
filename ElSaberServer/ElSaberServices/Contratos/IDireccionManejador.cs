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
    public interface IDireccionManejador
    {
        [OperationContract]
        int RegistrarDireccion(DireccionBinding direccion);
    }

    [DataContract]
    public class DireccionBinding
    {
        [DataMember]
        public int IdDireccion { get; set; }
        [DataMember]
        public string calle {  get; set; }
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public string codigoPostal { get; set; }
        [DataMember]
        public string ciudad { get; set; }
    }
}
