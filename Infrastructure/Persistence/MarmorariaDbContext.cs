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
        public DbSet<HistoricoEntrada> HistoricosEntrada { get; set; }
        public DbSet<HistoricoSaida> HistoricosSaida { get; set; }
    }
}