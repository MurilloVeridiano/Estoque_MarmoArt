namespace MarmorariaProjeto.UseCases.Movimentacao
{
    internal static class MovimentacaoRules
    {
        public static int GetStockDelta(string tipo, string ajusteAcao, int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("A quantidade deve ser maior que zero.");
            }

            return tipo switch
            {
                "Entrada" => quantidade,
                "Saída" or "Saida" => -quantidade,
                "Ajuste" when ajusteAcao == "Adicionar" => quantidade,
                "Ajuste" when ajusteAcao == "Retirar" => -quantidade,
                _ => throw new ArgumentException("Tipo de movimentação inválido.")
            };
        }
    }
}
