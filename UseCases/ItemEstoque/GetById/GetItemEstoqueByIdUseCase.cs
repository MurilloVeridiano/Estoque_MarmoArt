using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque.Register;

namespace MarmorariaProjeto.UseCases.ItemEstoque.GetById
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
                Nome = entidade.Nome,
                Quantidade = entidade.QuantidadeAtual
            };
        }
    }
}