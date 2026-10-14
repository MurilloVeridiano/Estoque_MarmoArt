using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Comunication.Responses;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.Movimentacao
{
    public class GetMovimentacaoById
    {
        private readonly MarmorariaDbContext _dbContext;

        public GetMovimentacaoById(MarmorariaDbContext context)
        {
            _dbContext = context;
        }

        public async Task<ResponseMovimentacaoJson> Execute(long id)
        {
            var movement = await _dbContext.MovimentacaoEstoque
                .Include(m => m.ItensEstoque)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movement == null)
            {
                throw new KeyNotFoundException("Movimentação não encontrada.");
            }

            return Map(movement);
        }

        internal static ResponseMovimentacaoJson Map(Domain.Entities.MovimentacaoEstoque movement)
        {
            return new ResponseMovimentacaoJson
            {
                Id = movement.Id,
                ItemEstoqueId = movement.ItemEstoqueId,
                Data = movement.Data,
                Tipo = movement.Tipo,
                AjusteAcao = movement.AjusteAcao,
                Categoria = movement.ItensEstoque?.Categoria ?? string.Empty,
                Item = movement.ItensEstoque?.Nome ?? string.Empty,
                FuncionarioPegou = movement.FuncionarioPegou,
                FuncionarioEntregou = movement.FuncionarioEntregou,
                Local = movement.Local,
                Quantidade = movement.Quantidade,
                Observacao = movement.Observacao
            };
        }
    }
}
