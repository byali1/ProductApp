using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.AddProductRange
{
    public class AddProductRangeCommandHandler : IRequestHandler<AddProductRangeCommand, ServiceResponse<List<Guid>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddProductRangeCommand> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddProductRangeCommandHandler(IProductRepository productRepository, IMapper mapper, IValidator<AddProductRangeCommand> validator, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<Guid>>> Handle(AddProductRangeCommand request, CancellationToken cancellationToken)
        {

            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return new ServiceResponse<List<Guid>>(Enumerable.Empty<Guid>().ToList())
                {
                    RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                    Message = $"{result.Errors.Select(x => x.ErrorMessage)}",
                    IsSuccess = false,
                    StatusCode = 400
                };
            }

            var products = _mapper.Map<List<Product>>(request.Products);
            await _productRepository.AddRangeAsync(products);

            var productIds = products.Select(x => x.Id).ToList();

            return new ServiceResponse<List<Guid>>(productIds)
            {
                RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                Message = "Product list added successfully"
            };
        }
    }
}
