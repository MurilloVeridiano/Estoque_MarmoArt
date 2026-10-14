using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class GetItemEstoqueByIdUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public GetItemEstoqueByIdUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResponseItemEstoqueJson Execute(long id)
        {
            // O EF Core busca a entidade no banco pelo ID
            var entidade = _dbContext.ItensEstoque.Find(id);

            if (entidade == null)
            {
                throw new ArgumentException("Item não encontrado.");
            }

            // Mapeia a entidade do banco para o JSON de resposta
            return new ResponseItemEstoqueJson
            {
                Id = entidade.Id,
                Categoria = entidade.Categoria,
                Nome = entidade.Nome,
                Quantidade = entidade.QuantidadeAtual,
                QuantidadeMinima = entidade.EstoqueMinimo,
                UnidadeMedida = entidade.UnidadeMedida,
                Fornecedor = entidade.Fornecedor
            };
        }
    }
}