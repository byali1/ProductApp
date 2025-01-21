using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ServiceResponse<Guid>> 
    {
        public Guid Id { get; set; }
        public UpdateProductViewModel UpdateProduct { get; set; }
    }
}
