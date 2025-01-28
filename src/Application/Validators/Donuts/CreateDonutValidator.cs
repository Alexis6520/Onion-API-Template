using Application.Commands.Donuts;
using FluentValidation;

namespace Application.Validators.Donuts
{
    /// <summary>
    /// Validador de creación de dona
    /// </summary>
    public class CreateDonutValidator : AbstractValidator<CreateDonutCommand>
    {
        public CreateDonutValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nombre obligatorio")
                .MaximumLength(50).WithMessage("Nombre demasiado largo");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Descripción demasiado larga");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("No se admiten precios negativos");
        }
    }
}
