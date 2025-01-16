using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductQuantityHandler : IRequestHandler<UpdateProductQuantityCommand, ServiceResponse<UpdateProductQuantityViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateProductQuantityHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UpdateProductQuantityViewModel>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updateProductQuantityVM = _mapper.Map<UpdateProductQuantityViewModel>(request);
                var result = await _productRepository.UpdateProductQuantityAsync(updateProductQuantityVM);

                return new ServiceResponse<UpdateProductQuantityViewModel>(result)
                {
                    Message = $"Product quantity updated to ({result.Quantity - request.Quantity} --> {result.Quantity}) successfully"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<UpdateProductQuantityViewModel>(_mapper.Map<UpdateProductQuantityViewModel>(request))
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}

