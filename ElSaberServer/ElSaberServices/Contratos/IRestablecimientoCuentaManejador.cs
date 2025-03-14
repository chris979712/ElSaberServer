using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Contratos
{
    [ServiceContract]
    public interface IRestablecimientoCuentaManejador
    {
        [OperationContract]
        int CorreoDeRestablecimientoDeContrasenia(string correo);
        [OperationContract]
        int RestablecimientoDeContrasenia(string correo);
        [OperationContract]
        bool VerificarCodigoDeVerificacion(string correo, string codigo);
    }
}
