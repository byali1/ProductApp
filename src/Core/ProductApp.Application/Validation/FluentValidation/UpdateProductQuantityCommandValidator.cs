using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using ProductApp.Application.Features.Commands.UpdateProduct;

namespace ProductApp.Application.Validation.FluentValidation
{
    public class UpdateProductQuantityCommandValidator : AbstractValidator<UpdateProductQuantityCommand>, IBaseFluentValidation
    {
        public UpdateProductQuantityCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.Quantity).NotNull().NotEmpty().WithMessage("Quantity is required");
        }
    }
}
