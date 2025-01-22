using AutoMapper;
using FluentValidation;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductCommand> _validator;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<UpdateProductCommand> validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ServiceResponse<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return new ServiceResponse<Guid>(Guid.Empty)
                {
                    Message = string.Join(", ", result.Errors.Select(x => x.ErrorMessage)),
                    IsSuccess = false
                };

            }

            var existingProduct = await _productRepository.GetByIdAsync(request.Id);

            if (existingProduct == null)
            {
                return new ServiceResponse<Guid>(default)
                {
                    IsSuccess = false,
                    Message = "Product not found"
                };
            }

            existingProduct.Name = request.UpdateProduct.Name;
            existingProduct.Price = request.UpdateProduct.Price;
            existingProduct.Quantity = request.UpdateProduct.Quantity;
            existingProduct.UpdatedDate = DateTimeOffset.UtcNow;

            await _productRepository.UpdateAsync(existingProduct);
            return new ServiceResponse<Guid>(existingProduct.Id)
            {
                Message = "Product updated successfully"
            };

        }
    }
}
