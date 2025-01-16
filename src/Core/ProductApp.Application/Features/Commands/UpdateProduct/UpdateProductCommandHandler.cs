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

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<Guid>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
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
            existingProduct.Value = request.UpdateProduct.Value;
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
