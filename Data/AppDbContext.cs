using Microsoft.EntityFrameworkCore;
using LocaControl.Models;

namespace LocaControl.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Equipamento>()
                .Property(e => e.ValorDiaria)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Locacao>()
                .Property(l => l.ValorTotal)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);
        }
    }
}