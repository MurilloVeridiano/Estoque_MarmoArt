using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Domain.Entities;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.Movimentacao
{
    public class RegisterMovimentacaoUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public RegisterMovimentacaoUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MovimentacaoEstoque> Execute(MovimentacaoRequest request)
        {
            ValidateRequest(request);
            var item = await _dbContext.ItensEstoque.FirstOrDefaultAsync(i => i.Id == request.ItemEstoqueId);
            if (item == null)
            {
                throw new KeyNotFoundException("Item de estoque não encontrado.");
            }

            var delta = MovimentacaoRules.GetStockDelta(request.Tipo, request.AjusteAcao, request.Quantidade);
            EnsureStockAvailable(item.QuantidadeAtual + delta);
            item.QuantidadeAtual += delta;

            var movement = CreateMovement(request);
            await _dbContext.MovimentacaoEstoque.AddAsync(movement);
            await _dbContext.SaveChangesAsync();
            return movement;
        }

        internal static void ValidateRequest(MovimentacaoRequest request)
        {
            if (request.ItemEstoqueId <= 0)
            {
                throw new ArgumentException("Selecione um item válido.");
            }

            if (string.IsNullOrWhiteSpace(request.FuncionarioPegou) ||
                string.IsNullOrWhiteSpace(request.FuncionarioEntregou) ||
                string.IsNullOrWhiteSpace(request.Local))
            {
                throw new ArgumentException("Funcionário, responsável e local são obrigatórios.");
            }

            _ = MovimentacaoRules.GetStockDelta(request.Tipo, request.AjusteAcao, request.Quantidade);
        }

        internal static void EnsureStockAvailable(int quantity)
        {
            if (quantity < 0)
            {
                throw new InvalidOperationException("Estoque insuficiente para esta movimentação.");
            }
        }

        internal static MovimentacaoEstoque CreateMovement(MovimentacaoRequest request)
        {
            var tipo = request.Tipo == "Saida" ? "Saída" : request.Tipo;
            return new MovimentacaoEstoque
            {
                ItemEstoqueId = request.ItemEstoqueId,
                Quantidade = request.Quantidade,
                Tipo = tipo,
                AjusteAcao = tipo == "Ajuste" ? request.AjusteAcao : string.Empty,
                Data = request.Data == default ? DateTime.UtcNow : DateTime.SpecifyKind(request.Data.Date, DateTimeKind.Utc),
                FuncionarioPegou = request.FuncionarioPegou.Trim(),
                FuncionarioEntregou = request.FuncionarioEntregou.Trim(),
                Local = request.Local.Trim(),
                Observacao = request.Observacao.Trim()
            };
        }
    }
}
