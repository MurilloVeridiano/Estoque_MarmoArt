using MarmorariaProjeto.Domain.Entities;

namespace MarmorariaProjeto.Domain.Entities
{
    public class HistoricoSaida
    {
        public long Id { get; set; }

        // Chave Estrangeira
        public long ItemEstoqueId { get; set; }
        public ItensEstoque? ItensEstoque { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
        public string Funcionario { get; set; } = string.Empty;
    }
}