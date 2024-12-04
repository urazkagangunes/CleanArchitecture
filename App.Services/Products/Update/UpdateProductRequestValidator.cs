using FluentValidation;

namespace App.Services.Products.Update;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(n => n.Name)
            .NotEmpty().WithMessage("Name has not be change empty")
            .Length(3, 10).WithMessage("Name has character between 3 - 10");

        RuleFor(p => p.Price)
          .GreaterThan(0).WithMessage("Price has to be bigger than 0.");

        RuleFor(s => s.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stock number has to be between 1 - 100.");
    }
}