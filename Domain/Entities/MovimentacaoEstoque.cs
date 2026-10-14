namespace MarmorariaProjeto.Domain.Entities
{

    public class MovimentacaoEstoque
    {
        public long Id { get; set; }

        public long ItemEstoqueId { get; set; }
        public ItensEstoque? ItensEstoque { get; set; } //Propriedade de navegação para o item de estoque relacionado

        public int Quantidade { get; set; }
        public string Tipo { get; set; } = string.Empty; // Entrada, Saída ou Ajuste
        public string AjusteAcao { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string FuncionarioPegou { get; set; } = string.Empty;
        public string FuncionarioEntregou { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty; // Essencial para justificar quebras ou sumiços
    }
}