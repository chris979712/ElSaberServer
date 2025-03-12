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
    public interface IUsuarioManejador
    {
        [OperationContract]
        int RegistrarUsuarioAlaBaseDeDatos(UsuarioBinding usuario, DireccionBinding direccion, AccesoBinding acceso);

        [OperationContract]
        int VerificarExistenciaDeUsuario(string correo, string telefonp);
    }

    [DataContract]
    public class UsuarioBinding
    {
        [DataMember]
        public int IdUsuario {  get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string primerApellido { get; set; }
        [DataMember]
        public string segundoApellido { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string puesto { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public DireccionBinding direccion { get; set; }
    }
}
