using Microsoft.EntityFrameworkCore;
using Rinha2024.Model;

namespace rinha.Infrastruture
{
    public class AppDBContext : DbContext
    {
        public DbSet<RetornoAtualizarSaldo> AtualizarSaldos => Set<RetornoAtualizarSaldo>();
        public DbSet<Extrato> Extrato => Set<Extrato>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Host=localhost;Port=5432;Database=db_rinha;Username=user_rinha;Password=passwd_rinha;Maximum Pool Size=600;Max Auto Prepare=20;Timeout=30";

            optionsBuilder.UseNpgsql(connectionString);
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);  //LOG DO SQL
            //optionsBuilder.EnableSensitiveDataLogging();  //MOSTRAR OS PARÃMETOS NO LOG SQL
            base.OnConfiguring(optionsBuilder);

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<RetornoAtualizarSaldo>().HasNoKey();
            modelBuilder.Entity<Extrato>().HasNoKey();
            base.OnModelCreating(modelBuilder);

        }
    }






}
