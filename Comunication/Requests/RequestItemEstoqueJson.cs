namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class RequestItemEstoqueJson //Aqui onde chega os dados
    {
        public string Categoria { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
        public int QuantidadeInicial { get; set; } //Vai ser a quantidade atual depois(aqui é para cadastro do produto)
        public int QuantidadeMinima { get; set; }
        public string UnidadeMedida { get; set; } = string.Empty;
        public string Fornecedor { get; set; } = string.Empty;
    }
}