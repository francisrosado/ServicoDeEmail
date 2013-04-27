using System;
using System.Collections.Generic;

namespace Dominio
{
    public class Remetente
    {
        public int RemetenteId { get; set; }
        public String DescricaoEmail { get; set; }
        public Byte Autenticacao { get; set; }
        public String Senha { get; set; }
        public String Smtp { get; set; }
        public int Porta { get; set; }

        public virtual IEnumerable<Mensagem> Mensagem { get; set; }
    }
}
