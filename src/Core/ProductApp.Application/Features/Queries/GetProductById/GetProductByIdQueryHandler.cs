using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<GetProductByIdViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<GetProductByIdViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product != null)
            {
                var productDto = _mapper.Map<GetProductByIdViewModel>(product);
                return new ServiceResponse<GetProductByIdViewModel>(productDto)
                {
                    RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                    Message = "Product retrieved successfully"
                };
            }

            return new ServiceResponse<GetProductByIdViewModel>(null)
            {
                RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                Message = "Product not found",
                IsSuccess = false,
                StatusCode = 404
            };

        }
    }
}
