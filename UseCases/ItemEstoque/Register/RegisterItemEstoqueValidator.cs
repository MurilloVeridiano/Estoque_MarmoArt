using FluentValidation;

namespace MarmorariaProjeto.UseCases.ItemEstoque.Register
{
    public class RegisterItemEstoqueValidator : AbstractValidator<RequestItemEstoqueJson>
    {
        public RegisterItemEstoqueValidator()
        {
            RuleFor(item => item.Nome).NotEmpty().WithMessage("O nome do item é obrigatório.");
            RuleFor(item => item.Quantidade).GreaterThan(0).WithMessage("A quantidade do item deve ser um valor positivo.");
        }
    }
}