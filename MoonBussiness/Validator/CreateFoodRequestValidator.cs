using FluentValidation;
using MoonModels.DTO.RequestDTO;

namespace MoonBussiness.Validator
{
    public class CreateFoodRequestValidator : AbstractValidator<CreateFoodRequest>
    {
        public CreateFoodRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Food name is required.");
            RuleFor(x => x.Price).GreaterThan(20000).WithMessage("Price must be greater than 20000.");
            RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid food type.");
        }
    }
}
