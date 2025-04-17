using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Contratos
{
    [ServiceContract]
    public interface IReporteMultasPagadas
    {
        [OperationContract]
        byte[] ObtenerReporteMultasPagadasEnFechas(string fechaInicioBusqueda, string fechaFinBusqueda);
    }
}
