using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProductRange
{
    public class UpdateProductRangeCommand : IRequest<ServiceResponse<List<Guid>>>
    {
        public List<UpdateProductRangeViewModel> Products { get; set; }
    }
}
