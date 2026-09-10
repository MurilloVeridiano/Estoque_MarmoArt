using MarmorariaProjeto.Infrastructure.Persistence;
using MarmorariaProjeto.UseCases.ItemEstoque.Register;

namespace MarmorariaProjeto.UseCases.ItemEstoque.Delete
{
    public class DeleteItemEstoqueUseCase
    {
        private readonly MarmorariaDbContext _dbContext;

        public DeleteItemEstoqueUseCase(MarmorariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ExecuteDelete(int id)
        {
            var entidade = _dbContext.ItensEstoque.Find(id);

            if (entidade != null)
            {
                _dbContext.ItensEstoque.Remove(entidade);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"ItemEstoque com ID {id} não encontrado.");
            }
        }
    }
}