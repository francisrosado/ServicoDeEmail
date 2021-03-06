﻿using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Dominio;

namespace Aplicacao
{
    public class EnviarEmail
    {
        public Boolean EnviarMensagemEmail(string remetente,
                                            string senhaRemetente,
                                            string smtpRemetente,
                                            int portaRemetente,
                                            string destinario,
                                            string assunto,
                                            string corpoDaMensagem,
                                            byte[] arquivoAnexo,
                                            int idMensagem)
        {
            try
            {
                var enderecoEmailValido = ValidarEmail(destinario);

                if (enderecoEmailValido == false)
                {
                    return false;
                }

                var mensagemEmail = new MailMessage
                    {
                        Sender = new MailAddress(remetente),
                        From = new MailAddress(remetente)
                    };
                mensagemEmail.To.Add(destinario);
                mensagemEmail.Subject = assunto;
                mensagemEmail.Body = corpoDaMensagem;

                var ms = new MemoryStream(arquivoAnexo);

                var anexado = new Attachment(ms, MediaTypeNames.Application.Octet);
                mensagemEmail.Attachments.Add(anexado);

                var clientSmtp = new SmtpClient(smtpRemetente, portaRemetente) { EnableSsl = false };

                var cread = new NetworkCredential(remetente, senhaRemetente);
                clientSmtp.Credentials = cread;
                clientSmtp.UseDefaultCredentials = true;

                clientSmtp.Send(mensagemEmail);

                return true;
            }
            catch (Exception)
            {
                return false;
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
