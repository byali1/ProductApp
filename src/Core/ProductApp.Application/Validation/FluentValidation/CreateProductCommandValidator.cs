using FluentValidation;
using ProductApp.Application.Dto;
using ProductApp.Application.Features.Commands.CreateProduct;

namespace ProductApp.Application.Validation.FluentValidation
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>, IBaseFluentValidation
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is required.");
            RuleFor(x => x.Name).MinimumLength(2).WithMessage("Product name must be 2 characters at least.");
            RuleFor(x => x.Name).MaximumLength(250).WithMessage("Product name can be up to 250 characters.");

            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

        }
    }
}
