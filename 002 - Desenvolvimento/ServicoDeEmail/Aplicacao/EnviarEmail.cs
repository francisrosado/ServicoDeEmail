using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Dominio;

namespace Aplicacao
{
    public class EnviarEmail
    {
        public string EnviarMensagemEmail(  string remetente, 
                                            string senhaRemetente, 
                                            string smtpRemetente, 
                                            int portaRemetente, 
                                            string destinario, 
                                            string assunto, 
                                            string corpoDaMensagem,
                                            string enderecoDoArquivoAnexo,
                                            int idMensagem)
        {
            var logApp = new LogAplicacao();

            try
            {
                var enderecoEmailValido = ValidarEmail(destinario);

                if (enderecoEmailValido == false)
                {
                    return "email invalido para";
                }

                var mensagemEmail = new MailMessage
                    {
                        Sender = new MailAddress(remetente),
                        From = new MailAddress(remetente)
                    };
                mensagemEmail.To.Add(destinario);
                mensagemEmail.Subject = assunto;
                mensagemEmail.Body = corpoDaMensagem;

                var anexado = new Attachment(enderecoDoArquivoAnexo, MediaTypeNames.Application.Octet);
                mensagemEmail.Attachments.Add(anexado);

                var clientSmtp = new SmtpClient(smtpRemetente, portaRemetente) {EnableSsl = false};

                var cread = new NetworkCredential(remetente, senhaRemetente);
                clientSmtp.Credentials = cread;
                clientSmtp.UseDefaultCredentials = true;

                clientSmtp.Send(mensagemEmail);

                return " Mensagem enviada de: " + remetente + " para: " + destinario;

            }
            catch (Exception)
            {
                var log = new Log
                {
                    Data = DateTime.Now,
                    Enviado = true,
                    MensagemDeEnvio = "OK",
                    Mensagem = { MensagemId = idMensagem }
                };

                logApp.Salvar(log);

                return "Deu Merda!!";
            }
        }


        public bool ValidarEmail(String enderecoEmail)
        {
            var textoValidar = enderecoEmail;

            var expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

            if (expressaoRegex.IsMatch(textoValidar))
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }

}
