public class ItemEstoque
{
    public int Id {get;private set;}
    public string Nome{get;set;}
    public int Quantidade{get; private set;}

    public string Fornecedor {get;private set;}

    public ItemEstoque(string nome, int quantidade, string fornecedor)
    {
        Nome = nome;
        Quantidade = quantidade;
        Fornecedor = fornecedor;
    }

    public void DarBaixa(int quantidade)
    {
        if(quantidade <= Quantidade)
        {
            Quantidade -= quantidade;
            Console.WriteLine($"Baixa realizada. Nova quantidade de {Nome}: {Quantidade}");
        }
        else
        {
            Console.WriteLine("Quantidade insuficiente em estoque.");
        }
    }

    public void Adicionar(string nome, int quantidade)
    {
        if(quantidade <= 0)
        {
            Console.WriteLine("Quantidade inválida. Não é possível adicionar uma quantidade menor ou igual a zero.");
            return;
        }else
        {
            Nome = nome;
            Quantidade += quantidade;
            Console.WriteLine($"Item {Nome} adicionado. Nova quantidade: {Quantidade}");
        }
    }
}
