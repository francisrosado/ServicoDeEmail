using System;
using System.IO;

namespace Aplicacao
{
    public class AnexoAplicacao
    {
        public string SalvarArquivoTempo(string nomeArquivo, Byte[] arquivoParaSalvar)
        {
            FileStream fs = null;
            BinaryWriter bw = null;

            var fileName = RenomeiaArquivo(nomeArquivo);
            var fileLocation = Path.GetTempPath() + fileName;

            DeletaArquivo(fileLocation);

            var byteArray = (byte[])arquivoParaSalvar;
            var ms = new MemoryStream(byteArray);
            //Aqui o processo para reescrver o array de bytes para recompor o arquivo.
            fs = new FileStream(fileLocation, FileMode.Create, FileAccess.ReadWrite);
            bw = new BinaryWriter(fs);
            bw.Write(ms.ToArray());
            bw.Close();
            fs.Close();
            ms.Close();

            return fileLocation;
        }


        public void DeletaArquivo(string fileLocation)
        { 
            try
            {
                 if (File.Exists(fileLocation))
                {
                    File.Delete(fileLocation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public string RenomeiaArquivo(string nomeArquivo)
        {
            nomeArquivo = nomeArquivo.Replace(".pdf", "");

            nomeArquivo = nomeArquivo + DateTime.Now;

            nomeArquivo = nomeArquivo.Replace(":", "");
            nomeArquivo = nomeArquivo.Replace("/", "_");
            nomeArquivo = nomeArquivo + ".pdf";
            return nomeArquivo;
        }
    }
}
