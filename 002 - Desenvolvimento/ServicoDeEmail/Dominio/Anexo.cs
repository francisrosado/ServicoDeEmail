using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Anexo
    {
        public int AnexoId{ get; set; }
        public String NomeArquivo { get; set; }
        public byte[] ArquivoAnexo { get; set; }

        public virtual IEnumerable<Mensagem> Mensagems { get; set; }
    }
}
