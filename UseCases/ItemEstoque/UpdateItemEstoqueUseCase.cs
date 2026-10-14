using Microsoft.EntityFrameworkCore;
using MarmorariaProjeto.Infrastructure.Persistence;

namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class UpdateItemEstoqueUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public UpdateItemEstoqueUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseItemEstoqueJson> Execute(long id, RequestItemEstoqueJson request)
        {
            var validator = new RegisterItemEstoqueValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                throw new ArgumentException(string.Join(" ", validation.Errors.Select(error => error.ErrorMessage)));
            }

            var item = await _dbContext.ItensEstoque.FirstOrDefaultAsync(i => i.Id == id);
            if (item == null)
            {
                throw new KeyNotFoundException("Item de estoque não encontrado.");
            }

            item.Categoria = request.Categoria.Trim();
            item.Nome = request.Item.Trim();
            item.EstoqueMinimo = request.QuantidadeMinima;
            item.UnidadeMedida = request.UnidadeMedida.Trim();
            item.Fornecedor = request.Fornecedor.Trim();
            await _dbContext.SaveChangesAsync();

            return new ResponseItemEstoqueJson
            {
                Id = item.Id,
                Categoria = item.Categoria,
                Nome = item.Nome,
                Quantidade = item.QuantidadeAtual,
                QuantidadeMinima = item.EstoqueMinimo,
                UnidadeMedida = item.UnidadeMedida,
                Fornecedor = item.Fornecedor
            };
        }
    }
}
