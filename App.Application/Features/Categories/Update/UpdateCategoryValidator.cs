using FluentValidation;

namespace App.Application.Features.Categories.Update;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(n => n.Name)
            .NotNull().WithMessage("Name has not be null.")
            .NotEmpty().WithMessage("Name has not be empty.")
            .Length(3, 10).WithMessage("Name has to character between 3 - 10");
    }
}