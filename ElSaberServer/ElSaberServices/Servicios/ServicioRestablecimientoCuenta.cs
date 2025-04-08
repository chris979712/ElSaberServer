using ElSaberDataAccess.Operaciones;
using ElSaberDataAccess.Utilities;
using ElSaberServices.Contratos;
using ElSaberServices.Utilidades;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ElSaberServices.Servicios
{
    public partial class ElSaberServices : IRestablecimientoCuentaManejador
    {
        private static Dictionary<string, string> _codigosDeVerificacion = new Dictionary<string, string>();
        public int CorreoDeRestablecimientoDeContrasenia(string correo)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoCorreoEnviado = Constantes.ErrorEnLaOperacion;
            string codigoGenerado = GenerarCodigoDeVerificacion(correo);
            string plantillaMensajeDeCorreo = FormatoCuerpoVerificacionDeMensaje();
            string correoRemitente = ConfigurationManager.AppSettings["Correo"];
            string contraseniaCorreo = ConfigurationManager.AppSettings["Contrasenia"];
            string servidorSmtp = ConfigurationManager.AppSettings["SmtpServer"];
            int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            try
            {
                if(plantillaMensajeDeCorreo != "PlantillaNoEncontrada")
                {
                    MailMessage mensajeAEnviar = new MailMessage();
                    mensajeAEnviar.Subject = "Solicitud de restablecimiento de contraseña";
                    mensajeAEnviar.From = new MailAddress(correoRemitente);
                    mensajeAEnviar.To.Add(correo);
                    mensajeAEnviar.Body = plantillaMensajeDeCorreo.Replace("{codigo}",codigoGenerado);
                    mensajeAEnviar.IsBodyHtml = true;
                    var smtpCliente = new SmtpClient(servidorSmtp)
                    {
                        Port = port,
                        Credentials = new NetworkCredential(correoRemitente, contraseniaCorreo),
                        EnableSsl = true
                    };
                    smtpCliente.Send(mensajeAEnviar);
                    resultadoCorreoEnviado = Constantes.OperacionExitosa;
                }
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                logger.LogError(fileNotFoundException);
            }
            catch (SmtpFailedRecipientException smtpFailedRecipientException)
            {
                logger.LogError(smtpFailedRecipientException);
            }
            catch (SmtpException smtpException)
            {
                logger.LogError(smtpException);
            }
            catch (FormatException formatException)
            {
                logger.LogError(formatException);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                logger.LogError(invalidOperationException);
            }
            return resultadoCorreoEnviado;
        }

        public int CorreoDeReenvioDeContrasenia(string correo, string contrasenia)
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            int resultadoCorreoEnviado = Constantes.ErrorEnLaOperacion;
            string plantillaMensajeDeCorreo = FormatoCuerpoReenvioContrasenia();
            string correoRemitente = ConfigurationManager.AppSettings["Correo"];
            string contraseniaCorreo = ConfigurationManager.AppSettings["Contrasenia"];
            string servidorSmtp = ConfigurationManager.AppSettings["SmtpServer"];
            int port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            try
            {
                if (plantillaMensajeDeCorreo != "PlantillaNoEncontrada")
                {
                    MailMessage mensajeAEnviar = new MailMessage();
                    mensajeAEnviar.Subject = "Contrasenia Restablecida";
                    mensajeAEnviar.From = new MailAddress(correoRemitente);
                    mensajeAEnviar.To.Add(correo);
                    mensajeAEnviar.Body = plantillaMensajeDeCorreo.Replace("{contrasenia}", contrasenia);
                    mensajeAEnviar.IsBodyHtml = true;
                    var smtpCliente = new SmtpClient(servidorSmtp)
                    {
                        Port = port,
                        Credentials = new NetworkCredential(correoRemitente, contraseniaCorreo),
                        EnableSsl = true
                    };
                    smtpCliente.Send(mensajeAEnviar);
                    resultadoCorreoEnviado = Constantes.OperacionExitosa;
                }
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                logger.LogError(fileNotFoundException);
            }
            catch (SmtpFailedRecipientException smtpFailedRecipientException)
            {
                logger.LogError(smtpFailedRecipientException);
            }
            catch (SmtpException smtpException)
            {
                logger.LogError(smtpException);
            }
            catch (FormatException formatException)
            {
                logger.LogError(formatException);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                logger.LogError(invalidOperationException);
            }
            return resultadoCorreoEnviado;
        }

        public int RestablecimientoDeContrasenia(string correo)
        {
            string contraseniaNueva = GeneradorContrasenia.GenerarContrasena();
            string contraseniaHasheada = Encriptado.hashToSHA2(contraseniaNueva);
            AccesoOperaciones accesoOperaciones = new AccesoOperaciones();
            int resultadoRestablecimiento = Constantes.ErrorEnLaOperacion;
            int resultadoModificacion = accesoOperaciones.ModificarContrasenia(correo, contraseniaHasheada);
            if (resultadoModificacion == 1)
            {
                int resultadoEnvioDeCorreo = CorreoDeReenvioDeContrasenia(correo, contraseniaNueva);
                if (resultadoEnvioDeCorreo == Constantes.OperacionExitosa)
                {
                    resultadoRestablecimiento = Constantes.OperacionExitosa;
                }
                else
                {
                    resultadoRestablecimiento = Constantes.ErrorAlEnviarCorreo;
                }
            }
            return resultadoRestablecimiento;
        }

        public string GenerarCodigoDeVerificacion(string correo)
        {
            if (_codigosDeVerificacion.ContainsKey(correo))
            {
                _codigosDeVerificacion.Remove(correo);
            }
            Random random = new Random();
            int codigoGenerado = random.Next(100000, 999999);
            string textoDeCodigoGenerado = codigoGenerado.ToString();
            _codigosDeVerificacion.Add(correo, textoDeCodigoGenerado);
            return textoDeCodigoGenerado;
        }

        public bool VerificarCodigoDeVerificacion(string correo,string codigoIngresado)
        {
            LoggerManager loggerManager = new LoggerManager(this.GetType());
            bool resultadoDeValidacion = false;
            try
            {
                string codigoAComparar = _codigosDeVerificacion[correo];
                if(codigoAComparar == codigoIngresado)
                {
                    resultadoDeValidacion = true;
                    _codigosDeVerificacion.Remove(correo);
                }
            }
            catch(KeyNotFoundException keyNotFoundException)
            {
                loggerManager.LogWarn(keyNotFoundException);
            }
            return resultadoDeValidacion;
        }

        public string FormatoCuerpoVerificacionDeMensaje()
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            string cuerpoDeMensaje;
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string serverPath = Path.GetFullPath(Path.Combine(baseDirectory, "../../../"));
                string templatePath = Path.Combine(serverPath, "ElSaberServices/Utilidades/CorreoRestablecimientoDeContrasenia.html");
                cuerpoDeMensaje = File.ReadAllText(templatePath);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                logger.LogError(fileNotFoundException);
                cuerpoDeMensaje = "PlantillaNoEncontrada";
            }
            return cuerpoDeMensaje;
        }

        public string FormatoCuerpoReenvioContrasenia()
        {
            LoggerManager logger = new LoggerManager(this.GetType());
            string cuerpoDeMensaje;
            try
            {
                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string serverPath = Path.GetFullPath(Path.Combine(baseDirectory, "../../../"));
                string templatePath = Path.Combine(serverPath, "ElSaberServices/Utilidades/CorreoContraseniaRestablecida.html");
                cuerpoDeMensaje = File.ReadAllText(templatePath);
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                logger.LogError(fileNotFoundException);
                cuerpoDeMensaje = "PlantillaNoEncontrada";
            }
            return cuerpoDeMensaje;
        }

    }
}
