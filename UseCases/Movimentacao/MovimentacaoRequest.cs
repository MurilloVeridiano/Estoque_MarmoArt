namespace MarmorariaProjeto.UseCases.Movimentacao
{
    public class MovimentacaoRequest
    {
        public long ItemEstoqueId { get; set; }
        public int Quantidade { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string AjusteAcao { get; set; } = string.Empty;
        public DateTime Data { get; set; }
        public string FuncionarioPegou { get; set; } = string.Empty;
        public string FuncionarioEntregou { get; set; } = string.Empty;
        public string Local { get; set; } = string.Empty;
        public string Observacao { get; set; } = string.Empty;
    }
}
