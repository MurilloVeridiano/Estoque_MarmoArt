using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.Movimentacao
{
    public class UpdateMovimentacaoUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public UpdateMovimentacaoUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Execute(long id, MovimentacaoRequest request)
        {
            RegisterMovimentacaoUseCase.ValidateRequest(request);
            var movement = await _dbContext.MovimentacaoEstoque
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movement == null)
            {
                throw new KeyNotFoundException("Movimentação não encontrada.");
            }

            var oldItem = await _dbContext.ItensEstoque
                .FirstOrDefaultAsync(item => item.Id == movement.ItemEstoqueId);
            var newItem = await _dbContext.ItensEstoque
                .FirstOrDefaultAsync(item => item.Id == request.ItemEstoqueId);
            if (oldItem == null || newItem == null)
            {
                throw new KeyNotFoundException("Item de estoque relacionado não encontrado.");
            }

            var oldDelta = MovimentacaoRules.GetStockDelta(movement.Tipo, movement.AjusteAcao, movement.Quantidade);
            var newDelta = MovimentacaoRules.GetStockDelta(request.Tipo, request.AjusteAcao, request.Quantidade);
            var oldItemNewQuantity = oldItem.QuantidadeAtual - oldDelta;
            var newItemNewQuantity = oldItem.Id == newItem.Id
                ? oldItemNewQuantity + newDelta
                : newItem.QuantidadeAtual + newDelta;

            RegisterMovimentacaoUseCase.EnsureStockAvailable(oldItemNewQuantity);
            RegisterMovimentacaoUseCase.EnsureStockAvailable(newItemNewQuantity);
            oldItem.QuantidadeAtual = oldItemNewQuantity;
            newItem.QuantidadeAtual = newItemNewQuantity;

            var updated = RegisterMovimentacaoUseCase.CreateMovement(request);
            movement.ItemEstoqueId = updated.ItemEstoqueId;
            movement.Quantidade = updated.Quantidade;
            movement.Tipo = updated.Tipo;
            movement.AjusteAcao = updated.AjusteAcao;
            movement.Data = updated.Data;
            movement.FuncionarioPegou = updated.FuncionarioPegou;
            movement.FuncionarioEntregou = updated.FuncionarioEntregou;
            movement.Local = updated.Local;
            movement.Observacao = updated.Observacao;
            await _dbContext.SaveChangesAsync();
        }
    }
}
