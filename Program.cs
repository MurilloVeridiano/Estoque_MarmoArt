class Program
{ 
    static void Main()
    {
        //Fazer um controle de estoque
        List<ItemEstoque> itensEstoque = new List<ItemEstoque>();

        short opcao;
        do
        {
            //Console.Clear();
            Console.WriteLine("=== Estoque === ");
            Console.WriteLine("Escolha uma opcao:");
            Console.WriteLine("[1] - Dar Baixa \n[2] - Listar \n[3] - Adicionar \n[4] - Sair");
            opcao = short.Parse(Console.ReadLine());

            switch(opcao)
            {
                case 1:
                    Console.WriteLine("Digite o nome do item que deseja dar baixa: ");
                    string nomeItemBaixa = Console.ReadLine();
                    ItemEstoque itemBaixa = itensEstoque.Find(item => item.Nome == nomeItemBaixa);
                    if (itemBaixa != null)
                    {
                        Console.WriteLine("Digite a quantidade a ser retirada: ");
                        int quantidadeBaixa = int.Parse(Console.ReadLine());
                        itemBaixa.DarBaixa(quantidadeBaixa);
                    }
                    else
                    {
                        Console.WriteLine("Item não encontrado.");
                    }
                    break;

                case 2:
                    Console.WriteLine("=== Estoque Atual ===");
                    foreach (var item in itensEstoque)
                    {
                        Console.WriteLine($"Item: {item.Nome}, Quantidade: {item.Quantidade}");
                    }
                    break;
                case 3:
                    Console.WriteLine("Digite o nome do item que deseja adicionar: ");
                    string nomeItemAdicionar = Console.ReadLine();
                    Console.WriteLine("Digite a quantidade a ser adicionada: ");
                    int quantidadeAdicionar = int.Parse(Console.ReadLine());
                    ItemEstoque itemAdicionar = itensEstoque.Find(item => item.Nome == nomeItemAdicionar);
                    if (itemAdicionar != null)
                    {
                        itemAdicionar.Adicionar(nomeItemAdicionar, quantidadeAdicionar);
                    }
                    else
                    {
                        ItemEstoque novoItem = new ItemEstoque(nomeItemAdicionar, quantidadeAdicionar, null);
                        itensEstoque.Add(novoItem);
                        Console.WriteLine($"Item {nomeItemAdicionar} adicionado ao estoque.");
                    }
                    break;
            }

        }while (opcao != 4);
    
        
    }

        
}