using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;
using ProductApp.Domain.Entities;

namespace ProductApp.Application.Features.Commands.UpdateProductRange
{
    public class UpdateProductRangeCommandHandler : IRequestHandler<UpdateProductRangeCommand, ServiceResponse<List<Guid>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public UpdateProductRangeCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
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
                    IsSuccess = false,
                    Message = "No products found to update."
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
                Message = "Product list updated successfully"
            };
        }
    }
}
