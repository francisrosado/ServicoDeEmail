using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
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
            Banco.Remetentes.Add(remetente);
            Banco.SaveChanges();
        }

        public IEnumerable<Remetente> Listar()
        {
            return Banco.Remetentes.ToList();
        }

        public IEnumerable<Remetente> Listar(int Identificador)
        {
            return Banco.Remetentes.Where(x => x.RemetenteId == Identificador).ToList();
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
