using Application.Commands.Donuts;
using FluentValidation;

namespace Application.Validators.Donuts
{
    public class CreateDonutValidator : AbstractValidator<CreateDonutCommand>
    {
        public CreateDonutValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Nombre requerido")
                .MaximumLength(50)
                .WithMessage("El nombre no puede exceder los {MaxLength} caracteres");

            RuleFor(x => x.Description)
                .MaximumLength(200)
                .WithMessage("La descripción no puede exceder los {MaxLength} caracteres");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El precio no puede ser menor a {ComparisonValue}");
        }
    }
}
