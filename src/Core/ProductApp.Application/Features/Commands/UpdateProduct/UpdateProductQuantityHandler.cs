using AutoMapper;
using FluentValidation;
using MediatR;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductQuantityHandler : IRequestHandler<UpdateProductQuantityCommand, ServiceResponse<UpdateProductQuantityViewModel>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateProductQuantityCommand> _validator;

        public UpdateProductQuantityHandler(IProductRepository productRepository, IMapper mapper, IValidator<UpdateProductQuantityCommand> validator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ServiceResponse<UpdateProductQuantityViewModel>> Handle(UpdateProductQuantityCommand request, CancellationToken cancellationToken)
        {
            var result = _validator.Validate(request);

            if (!result.IsValid)
            {
                return new ServiceResponse<UpdateProductQuantityViewModel>(default)
                {
                    Message = $"{result.Errors.Select(x => x.ErrorMessage)}",
                    IsSuccess = false
                };
            }

            var mappedResponse = _mapper.Map<UpdateProductQuantityViewModel>(request);
            var updatedResponse = await _productRepository.UpdateProductQuantityAsync(mappedResponse);

            return new ServiceResponse<UpdateProductQuantityViewModel>(updatedResponse)
            {
                Message = $"Product quantity updated to ({updatedResponse.Quantity - request.Quantity} --> {updatedResponse.Quantity}) successfully"
            };

        }
    }
}

