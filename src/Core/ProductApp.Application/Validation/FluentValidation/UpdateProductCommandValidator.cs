using FluentValidation;
using ProductApp.Application.Features.Commands.UpdateProduct;

namespace ProductApp.Application.Validation.FluentValidation
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>, IBaseFluentValidation
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id is required."); ;

            RuleFor(x => x.UpdateProduct.Quantity).NotEmpty().WithMessage("Quantity is required.")
                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.UpdateProduct.Name).NotEmpty().NotNull().WithMessage("Name is required");

            RuleFor(x => x.UpdateProduct.Price).NotEmpty().NotNull().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.");
        }
    }
}
