using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rinha.Domain.Model
{
    [Table("transacoes")]
    public class Transacoes
    {

        [Key] // Definindo a propriedade como chave primária
        [Column("id")] // Especificando o nome da coluna no banco de dados
        public int Id { get; set; }

        [Column("cliente_id")]
        public int Cliente_id { get; set; }

        [Column("valor")]
        public int Valor { get; set; }

        [Column("tipo")]
        public string Tipo { get; set; }

        [Column("descricao")]
        public string Descricao { get; set; }

        [Column("realizada_em")]
        public DateTime Realizada_em { get; set; }

        public Transacoes(int cliente_id, int valor, string tipo, string descricao)
        {
            Cliente_id = cliente_id;
            Valor = valor;
            Tipo = tipo;
            Descricao = descricao;
            Realizada_em = DateTime.UtcNow;
        }


    }
}