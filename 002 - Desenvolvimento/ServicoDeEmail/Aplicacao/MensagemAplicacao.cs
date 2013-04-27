using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dominio;
using Repositorio;

namespace Aplicacao
{
    public class MensagemAplicacao
    {
        public Contexto Banco { get; set; }

        public MensagemAplicacao()
        {
            Banco = new Contexto();
        }

        public IEnumerable<Mensagem> Listar()
        {
            return Banco.Mensagems
                .Include(x => x.Remetente)
                .Include(x => x.Destinario)
                .Include(x => x.Anexo).ToList();
        }
    }
}
