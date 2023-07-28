using FluentValidation;
using MoonBussiness.Validator;
using MoonModels.DTO.RequestDTO;

public class ComboRequestValidator : AbstractValidator<ComboRequest>
{
    public ComboRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Combo name is required.");
        RuleFor(x => x.Price).GreaterThan(5000).WithMessage("Price must be greater than 5000.");
        RuleForEach(x => x.Foods)
            .SetValidator(new CreateFoodRequestValidator());
    }
}
