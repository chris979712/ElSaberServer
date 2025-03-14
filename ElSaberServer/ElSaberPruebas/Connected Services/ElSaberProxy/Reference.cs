﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ElSaberPruebas.ElSaberProxy {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ElSaberProxy.IUsuarioManejador")]
    public interface IUsuarioManejador {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarioManejador/RegistrarUsuarioAlaBaseDeDatos", ReplyAction="http://tempuri.org/IUsuarioManejador/RegistrarUsuarioAlaBaseDeDatosResponse")]
        int RegistrarUsuarioAlaBaseDeDatos(ElSaberServices.Contratos.UsuarioBinding usuario, ElSaberServices.Contratos.DireccionBinding direccion, ElSaberServices.Contratos.AccesoBinding acceso);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarioManejador/RegistrarUsuarioAlaBaseDeDatos", ReplyAction="http://tempuri.org/IUsuarioManejador/RegistrarUsuarioAlaBaseDeDatosResponse")]
        System.Threading.Tasks.Task<int> RegistrarUsuarioAlaBaseDeDatosAsync(ElSaberServices.Contratos.UsuarioBinding usuario, ElSaberServices.Contratos.DireccionBinding direccion, ElSaberServices.Contratos.AccesoBinding acceso);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarioManejador/VerificarExistenciaDeUsuario", ReplyAction="http://tempuri.org/IUsuarioManejador/VerificarExistenciaDeUsuarioResponse")]
        int VerificarExistenciaDeUsuario(string correo, string telefonp);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUsuarioManejador/VerificarExistenciaDeUsuario", ReplyAction="http://tempuri.org/IUsuarioManejador/VerificarExistenciaDeUsuarioResponse")]
        System.Threading.Tasks.Task<int> VerificarExistenciaDeUsuarioAsync(string correo, string telefonp);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUsuarioManejadorChannel : ElSaberPruebas.ElSaberProxy.IUsuarioManejador, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UsuarioManejadorClient : System.ServiceModel.ClientBase<ElSaberPruebas.ElSaberProxy.IUsuarioManejador>, ElSaberPruebas.ElSaberProxy.IUsuarioManejador {
        
        public UsuarioManejadorClient() {
        }
        
        public UsuarioManejadorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UsuarioManejadorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuarioManejadorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UsuarioManejadorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int RegistrarUsuarioAlaBaseDeDatos(ElSaberServices.Contratos.UsuarioBinding usuario, ElSaberServices.Contratos.DireccionBinding direccion, ElSaberServices.Contratos.AccesoBinding acceso) {
            return base.Channel.RegistrarUsuarioAlaBaseDeDatos(usuario, direccion, acceso);
        }
        
        public System.Threading.Tasks.Task<int> RegistrarUsuarioAlaBaseDeDatosAsync(ElSaberServices.Contratos.UsuarioBinding usuario, ElSaberServices.Contratos.DireccionBinding direccion, ElSaberServices.Contratos.AccesoBinding acceso) {
            return base.Channel.RegistrarUsuarioAlaBaseDeDatosAsync(usuario, direccion, acceso);
        }
        
        public int VerificarExistenciaDeUsuario(string correo, string telefonp) {
            return base.Channel.VerificarExistenciaDeUsuario(correo, telefonp);
        }
        
        public System.Threading.Tasks.Task<int> VerificarExistenciaDeUsuarioAsync(string correo, string telefonp) {
            return base.Channel.VerificarExistenciaDeUsuarioAsync(correo, telefonp);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ElSaberProxy.IRestablecimientoCuentaManejador")]
    public interface IRestablecimientoCuentaManejador {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/CorreoDeRestablecimientoDeCon" +
            "trasenia", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/CorreoDeRestablecimientoDeCon" +
            "traseniaResponse")]
        int CorreoDeRestablecimientoDeContrasenia(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/CorreoDeRestablecimientoDeCon" +
            "trasenia", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/CorreoDeRestablecimientoDeCon" +
            "traseniaResponse")]
        System.Threading.Tasks.Task<int> CorreoDeRestablecimientoDeContraseniaAsync(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/RestablecimientoDeContrasenia" +
            "", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/RestablecimientoDeContrasenia" +
            "Response")]
        int RestablecimientoDeContrasenia(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/RestablecimientoDeContrasenia" +
            "", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/RestablecimientoDeContrasenia" +
            "Response")]
        System.Threading.Tasks.Task<int> RestablecimientoDeContraseniaAsync(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/VerificarCodigoDeVerificacion" +
            "", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/VerificarCodigoDeVerificacion" +
            "Response")]
        bool VerificarCodigoDeVerificacion(string correo, string codigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/VerificarCodigoDeVerificacion" +
            "", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/VerificarCodigoDeVerificacion" +
            "Response")]
        System.Threading.Tasks.Task<bool> VerificarCodigoDeVerificacionAsync(string correo, string codigo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/GenerarCodigoDeVerificacion", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/GenerarCodigoDeVerificacionRe" +
            "sponse")]
        string GenerarCodigoDeVerificacion(string correo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRestablecimientoCuentaManejador/GenerarCodigoDeVerificacion", ReplyAction="http://tempuri.org/IRestablecimientoCuentaManejador/GenerarCodigoDeVerificacionRe" +
            "sponse")]
        System.Threading.Tasks.Task<string> GenerarCodigoDeVerificacionAsync(string correo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRestablecimientoCuentaManejadorChannel : ElSaberPruebas.ElSaberProxy.IRestablecimientoCuentaManejador, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RestablecimientoCuentaManejadorClient : System.ServiceModel.ClientBase<ElSaberPruebas.ElSaberProxy.IRestablecimientoCuentaManejador>, ElSaberPruebas.ElSaberProxy.IRestablecimientoCuentaManejador {
        
        public RestablecimientoCuentaManejadorClient() {
        }
        
        public RestablecimientoCuentaManejadorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RestablecimientoCuentaManejadorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RestablecimientoCuentaManejadorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RestablecimientoCuentaManejadorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int CorreoDeRestablecimientoDeContrasenia(string correo) {
            return base.Channel.CorreoDeRestablecimientoDeContrasenia(correo);
        }
        
        public System.Threading.Tasks.Task<int> CorreoDeRestablecimientoDeContraseniaAsync(string correo) {
            return base.Channel.CorreoDeRestablecimientoDeContraseniaAsync(correo);
        }
        
        public int RestablecimientoDeContrasenia(string correo) {
            return base.Channel.RestablecimientoDeContrasenia(correo);
        }
        
        public System.Threading.Tasks.Task<int> RestablecimientoDeContraseniaAsync(string correo) {
            return base.Channel.RestablecimientoDeContraseniaAsync(correo);
        }
        
        public bool VerificarCodigoDeVerificacion(string correo, string codigo) {
            return base.Channel.VerificarCodigoDeVerificacion(correo, codigo);
        }
        
        public System.Threading.Tasks.Task<bool> VerificarCodigoDeVerificacionAsync(string correo, string codigo) {
            return base.Channel.VerificarCodigoDeVerificacionAsync(correo, codigo);
        }
        
        public string GenerarCodigoDeVerificacion(string correo) {
            return base.Channel.GenerarCodigoDeVerificacion(correo);
        }
        
        public System.Threading.Tasks.Task<string> GenerarCodigoDeVerificacionAsync(string correo) {
            return base.Channel.GenerarCodigoDeVerificacionAsync(correo);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ElSaberProxy.IAccesoManejador")]
    public interface IAccesoManejador {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccesoManejador/VerificarCredenciales", ReplyAction="http://tempuri.org/IAccesoManejador/VerificarCredencialesResponse")]
        int VerificarCredenciales(string correo, string telefono);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAccesoManejador/VerificarCredenciales", ReplyAction="http://tempuri.org/IAccesoManejador/VerificarCredencialesResponse")]
        System.Threading.Tasks.Task<int> VerificarCredencialesAsync(string correo, string telefono);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAccesoManejadorChannel : ElSaberPruebas.ElSaberProxy.IAccesoManejador, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AccesoManejadorClient : System.ServiceModel.ClientBase<ElSaberPruebas.ElSaberProxy.IAccesoManejador>, ElSaberPruebas.ElSaberProxy.IAccesoManejador {
        
        public AccesoManejadorClient() {
        }
        
        public AccesoManejadorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AccesoManejadorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccesoManejadorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AccesoManejadorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int VerificarCredenciales(string correo, string telefono) {
            return base.Channel.VerificarCredenciales(correo, telefono);
        }
        
        public System.Threading.Tasks.Task<int> VerificarCredencialesAsync(string correo, string telefono) {
            return base.Channel.VerificarCredencialesAsync(correo, telefono);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ElSaberProxy.IDireccionManejador")]
    public interface IDireccionManejador {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDireccionManejador/RegistrarDireccion", ReplyAction="http://tempuri.org/IDireccionManejador/RegistrarDireccionResponse")]
        int RegistrarDireccion(ElSaberServices.Contratos.DireccionBinding direccion);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDireccionManejador/RegistrarDireccion", ReplyAction="http://tempuri.org/IDireccionManejador/RegistrarDireccionResponse")]
        System.Threading.Tasks.Task<int> RegistrarDireccionAsync(ElSaberServices.Contratos.DireccionBinding direccion);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDireccionManejadorChannel : ElSaberPruebas.ElSaberProxy.IDireccionManejador, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DireccionManejadorClient : System.ServiceModel.ClientBase<ElSaberPruebas.ElSaberProxy.IDireccionManejador>, ElSaberPruebas.ElSaberProxy.IDireccionManejador {
        
        public DireccionManejadorClient() {
        }
        
        public DireccionManejadorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DireccionManejadorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DireccionManejadorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DireccionManejadorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int RegistrarDireccion(ElSaberServices.Contratos.DireccionBinding direccion) {
            return base.Channel.RegistrarDireccion(direccion);
        }
        
        public System.Threading.Tasks.Task<int> RegistrarDireccionAsync(ElSaberServices.Contratos.DireccionBinding direccion) {
            return base.Channel.RegistrarDireccionAsync(direccion);
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ElSaberProxy.ISocioManejador")]
    public interface ISocioManejador {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/RegistrarSocioEnBaseDeDatos", ReplyAction="http://tempuri.org/ISocioManejador/RegistrarSocioEnBaseDeDatosResponse")]
        int RegistrarSocioEnBaseDeDatos(ElSaberServices.Contratos.SocioBinding socio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/RegistrarSocioEnBaseDeDatos", ReplyAction="http://tempuri.org/ISocioManejador/RegistrarSocioEnBaseDeDatosResponse")]
        System.Threading.Tasks.Task<int> RegistrarSocioEnBaseDeDatosAsync(ElSaberServices.Contratos.SocioBinding socio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/VerificarExistenciaDeSocioEnBaseDeDatos", ReplyAction="http://tempuri.org/ISocioManejador/VerificarExistenciaDeSocioEnBaseDeDatosRespons" +
            "e")]
        int VerificarExistenciaDeSocioEnBaseDeDatos(string telefono);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/VerificarExistenciaDeSocioEnBaseDeDatos", ReplyAction="http://tempuri.org/ISocioManejador/VerificarExistenciaDeSocioEnBaseDeDatosRespons" +
            "e")]
        System.Threading.Tasks.Task<int> VerificarExistenciaDeSocioEnBaseDeDatosAsync(string telefono);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/ConsultarSociosPorNombre", ReplyAction="http://tempuri.org/ISocioManejador/ConsultarSociosPorNombreResponse")]
        ElSaberServices.Contratos.SocioBinding[] ConsultarSociosPorNombre(string nombre);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/ConsultarSociosPorNombre", ReplyAction="http://tempuri.org/ISocioManejador/ConsultarSociosPorNombreResponse")]
        System.Threading.Tasks.Task<ElSaberServices.Contratos.SocioBinding[]> ConsultarSociosPorNombreAsync(string nombre);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/ConsultarSocioPorNumeroDeSocio", ReplyAction="http://tempuri.org/ISocioManejador/ConsultarSocioPorNumeroDeSocioResponse")]
        ElSaberServices.Contratos.SocioBinding ConsultarSocioPorNumeroDeSocio(int numeroDeSocio);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISocioManejador/ConsultarSocioPorNumeroDeSocio", ReplyAction="http://tempuri.org/ISocioManejador/ConsultarSocioPorNumeroDeSocioResponse")]
        System.Threading.Tasks.Task<ElSaberServices.Contratos.SocioBinding> ConsultarSocioPorNumeroDeSocioAsync(int numeroDeSocio);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISocioManejadorChannel : ElSaberPruebas.ElSaberProxy.ISocioManejador, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SocioManejadorClient : System.ServiceModel.ClientBase<ElSaberPruebas.ElSaberProxy.ISocioManejador>, ElSaberPruebas.ElSaberProxy.ISocioManejador {
        
        public SocioManejadorClient() {
        }
        
        public SocioManejadorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SocioManejadorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SocioManejadorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SocioManejadorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int RegistrarSocioEnBaseDeDatos(ElSaberServices.Contratos.SocioBinding socio) {
            return base.Channel.RegistrarSocioEnBaseDeDatos(socio);
        }
        
        public System.Threading.Tasks.Task<int> RegistrarSocioEnBaseDeDatosAsync(ElSaberServices.Contratos.SocioBinding socio) {
            return base.Channel.RegistrarSocioEnBaseDeDatosAsync(socio);
        }
        
        public int VerificarExistenciaDeSocioEnBaseDeDatos(string telefono) {
            return base.Channel.VerificarExistenciaDeSocioEnBaseDeDatos(telefono);
        }
        
        public System.Threading.Tasks.Task<int> VerificarExistenciaDeSocioEnBaseDeDatosAsync(string telefono) {
            return base.Channel.VerificarExistenciaDeSocioEnBaseDeDatosAsync(telefono);
        }
        
        public ElSaberServices.Contratos.SocioBinding[] ConsultarSociosPorNombre(string nombre) {
            return base.Channel.ConsultarSociosPorNombre(nombre);
        }
        
        public System.Threading.Tasks.Task<ElSaberServices.Contratos.SocioBinding[]> ConsultarSociosPorNombreAsync(string nombre) {
            return base.Channel.ConsultarSociosPorNombreAsync(nombre);
        }
        
        public ElSaberServices.Contratos.SocioBinding ConsultarSocioPorNumeroDeSocio(int numeroDeSocio) {
            return base.Channel.ConsultarSocioPorNumeroDeSocio(numeroDeSocio);
        }
        
        public System.Threading.Tasks.Task<ElSaberServices.Contratos.SocioBinding> ConsultarSocioPorNumeroDeSocioAsync(int numeroDeSocio) {
            return base.Channel.ConsultarSocioPorNumeroDeSocioAsync(numeroDeSocio);
        }
    }
}
