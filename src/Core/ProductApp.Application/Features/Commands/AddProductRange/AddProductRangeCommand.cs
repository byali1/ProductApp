using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.AddProductRange
{
    public class AddProductRangeCommand :IRequest<ServiceResponse<List<Guid>>>
    {
        public List<ProductDto> Products { get; set; }
    }
}
