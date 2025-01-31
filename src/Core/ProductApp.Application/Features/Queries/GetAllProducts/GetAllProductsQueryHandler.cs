using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductApp.Application.Features.Queries.GetProductById;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ServiceResponse<List<GetProductByIdViewModel>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<List<GetProductByIdViewModel>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync();

            if (products.Count > 0)
            {
                var productsByIdVM = _mapper.Map<List<GetProductByIdViewModel>>(products);

                return new ServiceResponse<List<GetProductByIdViewModel>>(productsByIdVM)
                {
                    RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                    Message = "Product list returned successfully"
                };
            }

            return new ServiceResponse<List<GetProductByIdViewModel>>(null)
            {
                RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                Message = "No products found",
            };

        }
    }
}
