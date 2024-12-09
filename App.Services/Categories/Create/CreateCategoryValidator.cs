using App.Services.Categories.Create;
using FluentValidation;

namespace App.Services.Categories.Create;

public class CreateCategoryValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryValidator()
    {
        RuleFor(n => n.Name)
            .NotNull().WithMessage("Name has not be null.")
            .NotEmpty().WithMessage("Name has not be empty.")
            .Length(3, 10).WithMessage("Name has to character between 3 - 10");
    }
}