namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class ResponseItemEstoqueJson //Aqui onde eu mando os dados
    {
        public long Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
        public int QuantidadeMinima { get; set; }
        public string UnidadeMedida { get; set; } = "un";
        public string Fornecedor { get; set; } = string.Empty;
    }
}