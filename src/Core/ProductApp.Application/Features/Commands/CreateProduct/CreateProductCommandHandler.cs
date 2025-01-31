using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<CreateProductCommand> createProductCommandValidator, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _createProductCommandValidator = createProductCommandValidator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var result = _createProductCommandValidator.Validate(request);

            if (!result.IsValid)
            {
                return new ServiceResponse<Guid>(Guid.Empty)
                {
                    RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                    Message = $"{result.Errors.Select(x=> x.ErrorMessage)}",
                    IsSuccess = false,
                    StatusCode = 400
                };
            }

            var product = _mapper.Map<Product>(request);
            await _productRepository.AddAsync(product);
            return new ServiceResponse<Guid>(product.Id)
            {
                RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                Message = "Product created successfully"
            };
        }
    }
}
