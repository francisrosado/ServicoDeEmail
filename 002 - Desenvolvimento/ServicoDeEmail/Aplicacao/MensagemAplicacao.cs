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

        public void Alterar(Mensagem mensagem)
        {
            Mensagem mensagemParaSalvar = Banco.Mensagems.Where(x => x.MensagemId == mensagem.MensagemId).First();
            Banco.SaveChanges();
        }

        public void Alterar(Remetente remetente)
        {
            Remetente remetenteParaSalvar = Banco.Remetentes.Where(x => x.RemetenteId == remetente.RemetenteId).First();
            remetenteParaSalvar.RemetenteId = remetente.RemetenteId;
            remetenteParaSalvar.DescricaoEmail = remetente.DescricaoEmail;
            Banco.SaveChanges();
        }

        public IEnumerable<Mensagem> Listar()
        {
            return Banco.Mensagems.Where(c => c.Enviado == null)
                     .Include(x => x.Remetente)
                     .Include(x => x.Destinario)
                     .Include(x => x.Anexo)
                     .ToList();
        }
    }
}
