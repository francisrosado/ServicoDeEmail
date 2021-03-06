﻿using System;
using System.Collections.Generic;
using Aplicacao;
using Dominio;

namespace ServicoDeEmail
{
    internal class Program
    {
        private static void Main(string[] args)
        {
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
                var fileLocation = anexoApp.SalvarArquivoTempo(mensagens.Anexo.NomeArquivo,
                                                                  mensagens.Anexo.ArquivoAnexo);

                foreach (Destinario destinatarios in mensagens.Destinario)
                {
                    Console.WriteLine("Destinario: " + destinatarios.DescricaoEmail);

                    listaDestinatarios = listaDestinatarios + ", " + destinatarios.DescricaoEmail;
                }

                var statusDeEnvio = enviarEmail.EnviarMensagemEmail(mensagens.Remetente.DescricaoEmail,
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