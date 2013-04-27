using System;

namespace Dominio
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime Data { get; set; }
        public bool Enviado { get; set; }
        public string MensagemDeEnvio { get; set; }
        public Mensagem Mensagem { get; set; }
    }
}
