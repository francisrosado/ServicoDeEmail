using System;
using Aplicacao;
using Dominio;

namespace ServicoDeEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var mensagem = new Mensagem();
            var mensagemApp = new MensagemAplicacao();
            var enviarEmail = new EnviarEmail();
            var anexoApp = new AnexoAplicacao();
            var logApp = new LogAplicacao();
            var log = new Log();
            

            var listaDeMensagens = mensagemApp.Listar();
            if (listaDeMensagens == null) throw new ArgumentNullException("listaDeMensagens");
            foreach (var mensagens in listaDeMensagens)
            {
                var listaDestinatarios = "";
                var fileLocation = anexoApp.SalvarArquivoTempo(mensagens.Anexo.NomeArquivo, mensagens.Anexo.ArquivoAnexo);

                foreach (var destinatarios in mensagens.Destinario)
                {
                    Console.WriteLine("Destinario: " + destinatarios.DescricaoEmail);

                    listaDestinatarios = listaDestinatarios + ", " + destinatarios.DescricaoEmail;
                }

                Boolean statusDeEnvio = enviarEmail.EnviarMensagemEmail(mensagens.Remetente.DescricaoEmail,
                                                                        mensagens.Remetente.Senha,
                                                                        mensagens.Remetente.Smtp,
                                                                        mensagens.Remetente.Porta,
                                                                        listaDestinatarios,
                                                                        mensagens.Assunto,
                                                                        mensagens.CorpoDaMensagem,
                                                                        fileLocation,
                                                                        mensagens.MensagemId);

                mensagens.Enviado = "X";
                mensagemApp.Alterar(mensagens);

                log.Data = DateTime.Now;
                log.MensagemId = mensagens.MensagemId;
                log.Enviado = statusDeEnvio;
                log.MensagemDeEnvio = statusDeEnvio == true ? "Enviado" : "Falha";

                logApp.Salvar(log);
            }
        }
    }
}
