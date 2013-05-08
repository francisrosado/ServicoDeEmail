using System;
using System.Threading;
using System.IO;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Diagnostics;

using Aplicacao;
using Dominio;

namespace EmailService
{
    public partial class Service : ServiceBase
    {
        private Timer timer1;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer1 = new Timer(new TimerCallback(timer1_Tick), null, 15000, 600000);
        }

        protected override void OnStop()
        {
            StreamWriter vWriter = new StreamWriter(@"c:\EmailService.txt", true);

            vWriter.WriteLine("Servico Parado: " + DateTime.Now.ToString());
            vWriter.Flush();
            vWriter.Close();
        }

        private void timer1_Tick(object sender)
        {
            StreamWriter vWriter = new StreamWriter(@"c:\EmailService.txt", true);

            vWriter.WriteLine("Servico Rodando: " + DateTime.Now.ToString());
            vWriter.Flush();
            vWriter.Close();

            var mensagem = new Mensagem();
            var mensagemApp = new MensagemAplicacao();
            var enviarEmail = new EnviarEmail();
            var anexoApp = new AnexoAplicacao();
            var logApp = new LogAplicacao();
            var log = new Log();

            IEnumerable<Mensagem> listaDeMensagens = mensagemApp.Listar();

            foreach (Mensagem mensagens in listaDeMensagens)
            {
                var listaDestinatarios = "";

                foreach (Destinario destinatarios in mensagens.Destinario)
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
                                                                        mensagens.Anexo.ArquivoAnexo,
                                                                        mensagens.MensagemId);

                mensagens.Enviado = "X";
                mensagemApp.Alterar(mensagens);

                log.Data = DateTime.Now;
                log.MensagemId = mensagens.MensagemId;
                log.Enviado = statusDeEnvio;
                log.MensagemDeEnvio = statusDeEnvio ? "Enviado" : "Falha";

                logApp.Salvar(log);
            }
        }
    }
}
