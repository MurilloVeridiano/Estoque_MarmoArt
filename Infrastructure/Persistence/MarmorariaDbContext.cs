using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Domain.Entities;

namespace MarmorariaProjeto.Infrastructure.Persistence
{
    public class MarmorariaDbContext : DbContext
    {
        public MarmorariaDbContext(DbContextOptions<MarmorariaDbContext> options) : base(options)
        {
        }

        public DbSet<ItensEstoque> ItensEstoque { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacaoEstoque { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItensEstoque>()
                .Property(item => item.UnidadeMedida)
                .HasDefaultValue("un");

            modelBuilder.Entity<Login>()
                .HasIndex(login => login.Username)
                .IsUnique();

            modelBuilder.Entity<Login>()
                .Property(login => login.Username)
                .HasMaxLength(100);

            modelBuilder.Entity<MovimentacaoEstoque>()
                .HasOne(movement => movement.ItensEstoque)
                .WithMany()
                .HasForeignKey(movement => movement.ItemEstoqueId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}