namespace MarmorariaProjeto.Domain.Entities
{
    public class ItensEstoque
    {
        public long Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public int QuantidadeAtual { get; set; }
        public string Fornecedor { get; set; } = string.Empty;
        public int EstoqueMinimo { get; set; }
    

        public void RegistrarEntrada(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("A quantidade de entrada deve ser maior que zero.");
            }
            QuantidadeAtual += quantidade;
        }

        public void RegistrarSaída(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("A quantidade de saída deve ser maior que zero.");
            }
            if (quantidade > QuantidadeAtual)
            {
                throw new InvalidOperationException("Não é possível registrar uma saída maior que a quantidade atual em estoque.");
            }
            QuantidadeAtual -= quantidade;
        }
    }
}