using FluentValidation;
using ProductApp.Application.Features.Commands.AddProductRange;

namespace ProductApp.Application.Validation.FluentValidation
{
    public class AddProductRangeCommandValidator : AbstractValidator<AddProductRangeCommand>, IBaseFluentValidation
    {
        public AddProductRangeCommandValidator()
        {
            // Products listesindeki her bir öğe (ProductDto) için doğrulama kuralları
            RuleForEach(x => x.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MinimumLength(2).WithMessage("Product name must be at least 2 characters.")
                    .MaximumLength(250).WithMessage("Product name can be up to 250 characters.");

                product.RuleFor(p => p.Price)
                    .GreaterThan(0).WithMessage("Price must be greater than 0.");

                product.RuleFor(p => p.Quantity)
                    .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");
            });
        }
    }
}
