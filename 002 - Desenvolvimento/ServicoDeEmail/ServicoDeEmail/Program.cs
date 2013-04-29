using System;
using Aplicacao;
using Dominio;

namespace ServicoDeEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var mensagemApp = new MensagemAplicacao();
            var enviarEmail = new EnviarEmail();
            var anexoApp = new AnexoAplicacao();
            var logApp = new LogAplicacao();
            var log = new Log();

            var listaDeMensagens = mensagemApp.Listar();
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

                log.Data = DateTime.Now;
                log.MensagemId = mensagens.MensagemId;
                log.Enviado = statusDeEnvio;
                log.MensagemDeEnvio = statusDeEnvio == true ? "Enviado" : "Falha";

                logApp.Salvar(log);
            }

            #region
            //string stringConexao = @"driver={Firebird/Interbase(r) driver}; server=localhost; database=F:\Desenvolvimento\DataBases\FireBird\SCAM.FDB;uid=SYSDBA; pwd=masterkey";
            //string selecionaClientes = @"Select Endereco From email_usuario ";

            //using (OdbcConnection conexao = new OdbcConnection(stringConexao))
            //{
            //    conexao.Open();

            //    OdbcCommand comando = new OdbcCommand(selecionaClientes, conexao);

            //    OdbcDataReader resultado = comando.ExecuteReader();

            //    while (resultado.Read())
            //    {
            //        string nome = resultado["Endereco"] as string;
            //        Console.WriteLine(nome);
            //    }

            //    Console.ReadLine();
            //}
            #endregion


        }
    }
}
