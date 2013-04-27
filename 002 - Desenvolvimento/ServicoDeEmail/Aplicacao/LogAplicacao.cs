using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Repositorio;
using Dominio;

namespace Aplicacao
{
    public class LogAplicacao
    {
        public Contexto Banco { get; set; }

        public LogAplicacao()
        {
            Banco = new Contexto();       
        }

        public void Salvar(Log log)
        {
            Banco.Logs.Add(log);
            Banco.SaveChanges();
        }


    }
}
