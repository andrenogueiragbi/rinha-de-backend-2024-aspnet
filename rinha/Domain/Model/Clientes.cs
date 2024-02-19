using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rinha.Domain.Model
{
    [Table("clientes")]
    public class Clientes
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("limite")]
        public int Limite { get; set; }

        [Column("saldo")]
        public int Saldo { get; set; }
    }
}