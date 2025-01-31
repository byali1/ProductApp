using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProductRange
{
    public class UpdateProductRangeCommandHandler : IRequestHandler<UpdateProductRangeCommand, ServiceResponse<List<Guid>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateProductRangeCommandHandler(IProductRepository productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<List<Guid>>> Handle(UpdateProductRangeCommand request, CancellationToken cancellationToken)
        {
            var productDtos = request.Products;
            var productIds = productDtos.Select(p => p.Id).ToList();

            var productsToUpdate = await _productRepository.GetAllByIdsAsync(productIds);

            if (productsToUpdate == null || !productsToUpdate.Any())
            {
                return new ServiceResponse<List<Guid>>(default)
                {
                    RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                    IsSuccess = false,
                    Message = "No products found to update.",
                    StatusCode = 404
                };
            }

            foreach (var product in productsToUpdate)
            {
                var dto = productDtos.FirstOrDefault(p => p.Id == product.Id);

                if (dto != null)
                {
                    product.Name = dto.Name;
                    product.Price = dto.Price;
                    product.Quantity = dto.Quantity;
                    product.UpdatedDate = DateTimeOffset.UtcNow;
                }
            }

            await _productRepository.UpdateRangeAsync(productsToUpdate);

            return new ServiceResponse<List<Guid>>(productIds)
            {
                RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                Message = "Product list updated successfully"
            };
        }
    }
}
