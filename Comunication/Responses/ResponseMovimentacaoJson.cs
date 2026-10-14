namespace MarmorariaProjeto.Comunication.Responses
{
    public class ResponseMovimentacaoJson
    {
        public long Id { get; set; }
        public long ItemEstoqueId { get; set; }
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string Tipo { get; set; } = string.Empty;
        public string AjusteAcao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
        public string FuncionarioPegou { get; set; } = string.Empty;
        public string FuncionarioEntregou { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public string Observacao { get; set; } = string.Empty; // Essencial para justificar quebras ou sumiços
    }
}