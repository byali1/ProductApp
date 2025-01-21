using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.AddProductRange
{
    public class AddProductRangeCommandHandler : IRequestHandler<AddProductRangeCommand, ServiceResponse<List<Guid>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public AddProductRangeCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Guid>>> Handle(AddProductRangeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var products = _mapper.Map<List<Product>>(request.Products);
                await _productRepository.AddRangeAsync(products);

                var productIds = products.Select(x => x.Id).ToList();

                return new ServiceResponse<List<Guid>>(productIds)
                {
                    Message = "Product list added successfully"
                };
            }
            catch (Exception e)
            {
                return new ServiceResponse<List<Guid>>(default)
                {
                    IsSuccess = false,
                    Message = "Product list could not add"
                };
            }
        }
    }
}
