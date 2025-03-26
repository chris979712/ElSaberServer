using ElSaberDataAccess;
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
    public interface ISocioManejador
    {
        [OperationContract]
        int RegistrarSocioEnBaseDeDatos(SocioBinding socio);
        [OperationContract]
        int VerificarExistenciaDeSocioEnBaseDeDatos(string telefono);
        [OperationContract]
        List<SocioBinding> ConsultarSociosPorNombre(string nombre);
        [OperationContract]
        SocioBinding ConsultarSocioPorNumeroDeSocio(int numeroDeSocio);
        [OperationContract]
        int ModificarEstadoSocio(int numeroDeSocio, string estado);
    }

    [DataContract] 
    public class SocioBinding
    {
        [DataMember]
        public int numeroDeSocio {  get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string primerApellido { get; set; }
        [DataMember]
        public string segundoApellido { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public DateTime fechaDeInscripcion {  get; set; }
        [DataMember]
        public DateTime fechaDeNacimiento {  get; set; }
        [DataMember]
        public DireccionBinding direccion { get; set; }
    }
}
