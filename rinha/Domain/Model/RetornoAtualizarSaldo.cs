using System.ComponentModel.DataAnnotations;

namespace Rinha2024.Model
{
    public class RetornoAtualizarSaldo
    {
        public int  saldo { get; set; }
        public int  limite_cliente { get; set; }
        public bool falha { get; set; }
        public string? mensagem { get; set; }


    }
}


//(saldo INT, limite_cliente INT, falha BOOLEAN, mensagem VARCHAR(10)) 