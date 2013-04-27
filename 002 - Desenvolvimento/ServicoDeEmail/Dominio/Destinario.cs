using System.Collections.Generic;

namespace Dominio
{
    public class Destinario
    {
        public int DestinarioId { get; set; }
        public string DescricaoEmail { get; set; }
        public List<Mensagem> Mensagems { get; set; }
    }
}
