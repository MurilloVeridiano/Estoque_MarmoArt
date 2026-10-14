using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque;

namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class GetItemEstoqueAllUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public GetItemEstoqueAllUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ResponseItemEstoqueJson> ExecuteGetAll()
        {
            // O EF Core busca todas as entidades no banco
            var entidades = _dbContext.ItensEstoque.ToList();

            // Mapeia as entidades do banco para o JSON de resposta
            return entidades.Select(entidade => new ResponseItemEstoqueJson
            {
                Id = entidade.Id,
                Categoria = entidade.Categoria,
                Nome = entidade.Nome,
                Quantidade = entidade.QuantidadeAtual,
                QuantidadeMinima = entidade.EstoqueMinimo,
                UnidadeMedida = entidade.UnidadeMedida,
                Fornecedor = entidade.Fornecedor
            });
        }
    }
}