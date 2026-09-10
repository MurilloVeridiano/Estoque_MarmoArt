using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque.Register;

namespace MarmorariaProjeto.UseCases.ItemEstoque.GetAll
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
                Nome = entidade.Nome,
                Quantidade = entidade.QuantidadeAtual
            });
        }
    }
}