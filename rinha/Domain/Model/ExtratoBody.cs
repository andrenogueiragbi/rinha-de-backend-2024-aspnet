namespace rinha.Domain.Model
{
    public class ExtartoBody
    {
        public class Saldo
        {
            public int total { get; set; }
            public int limite { get; set; }
        }

        public class Transacao
        {
            public int valor { get; set; }
            public string tipo { get; set; }
            public string descricao { get; set; }
            public DateTime realizada_em { get; set; }
        }

        public Saldo saldo { get; set; }
        public Transacao[] ultimas_transacoes { get; set; }
    }

}