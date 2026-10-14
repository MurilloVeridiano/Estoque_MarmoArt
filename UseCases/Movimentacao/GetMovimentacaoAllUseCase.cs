using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Comunication.Responses;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.Movimentacao
{
    public class GetMovimentacaoAllUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public GetMovimentacaoAllUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<ResponseMovimentacaoJson>> Execute()
        {
            var movements = await _dbContext.MovimentacaoEstoque
                .Include(movement => movement.ItensEstoque)
                .OrderByDescending(movement => movement.Data)
                .ThenByDescending(movement => movement.Id)
                .ToListAsync();

            return movements.Select(GetMovimentacaoById.Map).ToList();
        }
    }
}
