using System;
using Aplicacao;

namespace ServicoDeEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            var mensagemApp = new MensagemAplicacao();
            var enviarEmail = new EnviarEmail();
            var anexoApp = new AnexoAplicacao();

            var listaDeMensagens = mensagemApp.Listar() ;
            foreach (var mensagens in listaDeMensagens )
            {
                var listaDestinatarios = "";

                var fileLocation = anexoApp.SalvarArquivoTempo(mensagens.Anexo.NomeArquivo, mensagens.Anexo.ArquivoAnexo);
                 
                Console.WriteLine("Mensagem:" + mensagens.Assunto);
                Console.WriteLine("Remetente:" +  mensagens.Remetente.DescricaoEmail);

                foreach (var destinatarios in mensagens.Destinario)
                {
                    Console.WriteLine("Destinario: " + destinatarios.DescricaoEmail);

                    listaDestinatarios = listaDestinatarios + ", " + destinatarios.DescricaoEmail ;
                }

                Console.WriteLine(enviarEmail.EnviarMensagemEmail(  mensagens.Remetente.DescricaoEmail,
                                                                    mensagens.Remetente.Senha,
                                                                    mensagens.Remetente.Smtp,
                                                                    mensagens.Remetente.Porta,
                                                                    listaDestinatarios,
                                                                    mensagens.Assunto,
                                                                    mensagens.CorpoDaMensagem,
                                                                    fileLocation,
                                                                    mensagens.MensagemId));
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
