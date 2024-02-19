using Microsoft.EntityFrameworkCore;
using rinha.Domain.Model;

namespace rinha.Infrastruture
{
    public class AppDBContext : DbContext
    {

        public DbSet<Clientes> clientes { get; set; }
        public DbSet<Transacoes> transacoes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Host=localhost;Port=5432;Database=db_rinha;Username=user_rinha;Password=passwd_rinha;Maximum Pool Size=600;Max Auto Prepare=20;Timeout=30";

            optionsBuilder.UseNpgsql(connectionString);
            //optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }

    }
}