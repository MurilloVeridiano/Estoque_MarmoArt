using FluentValidation;

namespace MarmorariaProjeto.UseCases.ItemEstoque
{
    public class RegisterItemEstoqueValidator : AbstractValidator<RequestItemEstoqueJson>
    {
        public RegisterItemEstoqueValidator()
        {
            RuleFor(item => item.Categoria).NotEmpty().WithMessage("A categoria é obrigatória.");
            RuleFor(item => item.Item).NotEmpty().WithMessage("O nome do item é obrigatório.");
            RuleFor(item => item.QuantidadeInicial).GreaterThanOrEqualTo(0).WithMessage("A quantidade inicial não pode ser negativa.");
            RuleFor(item => item.QuantidadeMinima).GreaterThanOrEqualTo(0).WithMessage("A quantidade mínima não pode ser negativa.");
            RuleFor(item => item.UnidadeMedida).NotEmpty().WithMessage("A unidade de medida é obrigatória.");
            RuleFor(item => item.Fornecedor).NotEmpty().WithMessage("O fornecedor é obrigatório.");
        }
    }
}