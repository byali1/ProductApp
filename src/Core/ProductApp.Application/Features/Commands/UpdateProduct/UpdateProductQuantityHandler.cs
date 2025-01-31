using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductQuantityHandler : IRequestHandler<UpdateProductQuantityCommand, ServiceResponse<UpdateProductQuantityViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductQuantityCommand> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UpdateProductQuantityHandler(IProductRepository productRepository, IMapper mapper, IValidator<UpdateProductQuantityCommand> validator, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<UpdateProductQuantityViewModel>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return new ServiceResponse<UpdateProductQuantityViewModel>(default)
                {
                    RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                    Message = $"{result.Errors.Select(x => x.ErrorMessage)}",
                    IsSuccess = false,
                    StatusCode = 400
                };
            }

            var mappedResponse = _mapper.Map<UpdateProductQuantityViewModel>(request);
            var updatedResponse = await _productRepository.UpdateProductQuantityAsync(mappedResponse);

            return new ServiceResponse<UpdateProductQuantityViewModel>(updatedResponse)
            {
                RequestId = _httpContextAccessor.HttpContext.TraceIdentifier,
                Message = $"Product quantity updated to ({updatedResponse.Quantity - request.Quantity} --> {updatedResponse.Quantity}) successfully"
            };

        }
    }
}

