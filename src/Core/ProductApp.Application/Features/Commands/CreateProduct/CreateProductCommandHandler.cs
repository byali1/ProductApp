using AutoMapper;
using FluentValidation;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductCommand> _createProductCommandValidator;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<CreateProductCommand> createProductCommandValidator)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
            _createProductCommandValidator = createProductCommandValidator;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = _createProductCommandValidator.Validate(request);

            if (!result.IsValid)
            {
                return new ServiceResponse<Guid>(Guid.Empty)
                {
                    Message = $"{result.Errors.Select(x=> x.ErrorMessage)}",
                    IsSuccess = false
                };
            }

            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            return new ServiceResponse<Guid>(product.Id)
            {
                Message = "Product created successfully"
            };
        }
    }
}
