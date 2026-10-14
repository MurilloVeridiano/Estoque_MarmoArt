using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.Movimentacao
{
    public class DeleteMovimentacaoUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public DeleteMovimentacaoUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(long id)
        {
            var movement = await _dbContext.MovimentacaoEstoque
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement == null)
            {
                throw new KeyNotFoundException("Movimentação não encontrada.");
            }

            var item = await _dbContext.ItensEstoque
                .FirstOrDefaultAsync(i => i.Id == movement.ItemEstoqueId);
            if (item == null)
            {
                throw new KeyNotFoundException("Item de estoque relacionado não encontrado.");
            }

            var delta = MovimentacaoRules.GetStockDelta(movement.Tipo, movement.AjusteAcao, movement.Quantidade);
            RegisterMovimentacaoUseCase.EnsureStockAvailable(item.QuantidadeAtual - delta);
            item.QuantidadeAtual -= delta;
            _dbContext.MovimentacaoEstoque.Remove(movement);
            await _dbContext.SaveChangesAsync();
        }
    }
}
