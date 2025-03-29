﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Contratos
{
    [ServiceContract]
    public interface IReporteLibroMasPrestadoManejador
    {
        [OperationContract]
        byte[] ObtenerReporteLibrosMasPrestado(string fechaInicioBusqueda, string fechaFinBusqueda);
    }
}
