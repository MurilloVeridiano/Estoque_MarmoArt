using MarmorariaProjeto.Domain.Entities;
using MarmorariaProjeto.Infrastructure.Persistence;

//Cadastra novos produtos no estoque, recebendo o nome e a quantidade do produto. Retorna o nome e a quantidade cadastrada.

namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class RegisterItemEstoqueUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public RegisterItemEstoqueUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResponseItemEstoqueJson Execute(RequestItemEstoqueJson request)
        {
            var validator = new RegisterItemEstoqueValidator();
            var result = validator.Validate(request);
            
            if(result.IsValid == false)
            {
                throw new ArgumentException("Erro nos Dados recebidos");
            }

            var entidade = new ItensEstoque
            {
                Categoria = request.Categoria.Trim(),
                Nome = request.Item.Trim(),
                QuantidadeAtual = request.QuantidadeInicial,
                EstoqueMinimo = request.QuantidadeMinima,
                UnidadeMedida = request.UnidadeMedida.Trim(),
                Fornecedor = request.Fornecedor.Trim()
            };

            _dbContext.ItensEstoque.Add(entidade);
            _dbContext.SaveChanges();

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