namespace MarmorariaProjeto.UseCases.ItemEstoque.Register
{
    public class RequestItemEstoqueJson //Aqui onde chega os dados
    {
        public string Nome { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }
}