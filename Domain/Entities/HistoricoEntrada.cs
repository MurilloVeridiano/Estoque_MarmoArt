using MarmorariaProjeto.Domain.Entities;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.Domain.Entities
{
    public class HistoricoEntrada
    {
        public long Id { get; set; }
        public long ItemEstoqueId { get; set; }
        public ItensEstoque? ItensEstoque { get; set; }
        public int Quantidade { get; set; }
        public DateTime Data { get; set; }
    

        public void RegistrarEntrada(int quantidade)
        {
            if (quantidade <= 0)
            {
                throw new ArgumentException("A quantidade de entrada deve ser maior que zero.");
            }
            Quantidade += quantidade;
        }
    }
}
