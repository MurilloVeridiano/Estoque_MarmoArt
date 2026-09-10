using MarmorariaProjeto.Domain.Entities;
using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque.Register;

namespace MarmorariaProjeto.UseCases.ItemEstoque.Register
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

            // Usa a classe ItensEstoque que já está na sua pasta Domain.Entities
            var entidade = new ItensEstoque
            {
                Nome = request.Nome,
                QuantidadeAtual = request.Quantidade
            };

            _dbContext.ItensEstoque.Add(entidade);
            _dbContext.SaveChanges();

            return new ResponseItemEstoqueJson
            {
                Nome = entidade.Nome,
                Quantidade = entidade.QuantidadeAtual
            };
        }
    }
}