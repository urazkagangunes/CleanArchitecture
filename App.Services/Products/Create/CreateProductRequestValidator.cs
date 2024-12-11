using FluentValidation;

namespace App.Services.Products.Create;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(n => n.Name)
            .NotNull().WithMessage("Name has not be null.")
            .NotEmpty().WithMessage("Name has not be empty.")
            .Length(3, 10).WithMessage("Name has to character between 3 - 10");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price has to be bigger than 0.");

        RuleFor(c => c.CategoryId)
            .GreaterThan(0).WithMessage("Category Id must be bigger than 0");

        RuleFor(s => s.Stock)
            .InclusiveBetween(1, 100).WithMessage("Stock number has to be between 1 - 100.");
    }
}
