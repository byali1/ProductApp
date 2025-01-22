using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductQuantityCommand : UpdateProductQuantityViewModel, IRequest<ServiceResponse<UpdateProductQuantityViewModel>>
    {
        
    }
}
