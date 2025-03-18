using ElSaberDataAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Contratos
{
    [ServiceContract]
    public interface IAccesoManejador
    {
        [OperationContract]
        int VerificarCredenciales(string correo, string telefono);
        [OperationContract]
        int VerificarCorreoExistente(string correo);
        [OperationContract]
        int ModificarContrasenia(string correo, string contrasenia);

        [OperationContract]
        AccesoBinding IniciarSesion(string correo, string contrasenia);
    }

    [DataContract]
    public class AccesoBinding
    {
        [DataMember]
        public int IdAcceso { get; set; }
        [DataMember]
        public string correo { get; set; }
        [DataMember]
        public string contrasenia { get; set; }
        [DataMember]
        public string tipoDeUsuario { get; set; }
        [DataMember]
        public UsuarioBinding IdUsuario { get; set; }  
    }
}
