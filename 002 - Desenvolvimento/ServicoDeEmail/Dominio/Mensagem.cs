using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Mensagem
    {
        public int MensagemId { get; set; }
        public String Assunto { get; set; }
        public String CorpoDaMensagem { get; set; }
        public String Enviado { get; set; }

        public Remetente Remetente { get; set; }
        public Anexo Anexo { get; set; }

        public virtual IEnumerable<Log> Log { get; set; }
        public List<Destinario> Destinario { get; set; }
    }
}
