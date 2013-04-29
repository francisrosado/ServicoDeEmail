using System.Collections.Generic;
using System.Linq;
using Dominio;
using Repositorio;

namespace Aplicacao
{
    public class RemetenteAplicacao
    {
        public Contexto Banco { get; set; }

        public RemetenteAplicacao()
        {
            Banco = new Contexto();
        }

        public void Salvar(Remetente remetente)
        {
            if (remetente != null) Banco.Remetentes.Add(remetente);
            Banco.SaveChanges();
        }

        public IEnumerable<Remetente> Listar()
        {
            return Banco.Remetentes.ToList();
        }

        public IEnumerable<Remetente> Listar(int identificador)
        {
            return Banco.Remetentes.Where(x => x.RemetenteId == identificador).ToList();
        }

        public void Alterar(Remetente remetente)
        {
            Remetente remetenteParaSalvar = Banco.Remetentes.Where(x => x.RemetenteId == remetente.RemetenteId).First();
            remetenteParaSalvar.RemetenteId = remetente.RemetenteId;
            remetenteParaSalvar.DescricaoEmail = remetente.DescricaoEmail;
            Banco.SaveChanges();
        }
    }
}
