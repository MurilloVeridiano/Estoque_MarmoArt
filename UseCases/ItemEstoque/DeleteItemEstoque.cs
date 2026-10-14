using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque;

namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class DeleteItemEstoqueUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public DeleteItemEstoqueUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ExecuteDelete(long id)
        {
            var entidade = _dbContext.ItensEstoque.Find(id);

            if (entidade == null)
            {
                throw new KeyNotFoundException($"Item de estoque com ID {id} não encontrado.");
            }

            if (entidade.QuantidadeAtual != 0)
            {
                throw new InvalidOperationException("Só é possível excluir itens sem quantidade em estoque.");
            }

            if (_dbContext.MovimentacaoEstoque.Any(movement => movement.ItemEstoqueId == id))
            {
                throw new InvalidOperationException("Não é possível excluir um item que possui histórico de movimentações.");
            }

            _dbContext.ItensEstoque.Remove(entidade);
            _dbContext.SaveChanges();
        }
    }
}