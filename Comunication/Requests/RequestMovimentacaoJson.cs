namespace MarmorariaProjeto.Comunication.Resquests
{
    public class RequestMovimentacaoJson
    {
        public DateTime Data { get; set; }
        public string TipoMovimentacao { get; set; } = "Todas as Categorias";
        public string Categoria { get; set; } = "Todas as Categorias";
        public string Item { get; set; } = "Todos os Itens";
        public string FuncionarioPegou { get; set; } = string.Empty;
        public string FuncionarioEntregou { get; set; } = string.Empty;
        public int? Quantidade { get; set; }
        public string Observacao { get; set; } = string.Empty;

    }
}