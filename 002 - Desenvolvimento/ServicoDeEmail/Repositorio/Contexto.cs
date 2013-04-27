using System.Data.Entity;
using Dominio;

namespace Repositorio
{
    public class Contexto : DbContext
    {
        public DbSet<Remetente> Remetentes { get; set; } 
        public DbSet<Mensagem> Mensagems { get; set; }
        public DbSet<Destinario> Destinarios { get; set; }
        public DbSet<Anexo> Anexos { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
