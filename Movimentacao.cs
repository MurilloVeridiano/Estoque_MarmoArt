public class Movimentacao
{
    public string Responsavel {get;private set;}
    public string Funcionario {get; private set;}
    public string Item {get; private set;}
    public int Quantidade {get; private set;}
    public DateTime Data {get; private set;}

    public Movimentacao(string resposavel, string funcionario, string item, int quantidade)
    {
        Responsavel = resposavel;
        Funcionario = funcionario;
        Item = item;
        Quantidade = quantidade;
        Data = DateTime.Now;
    }
}