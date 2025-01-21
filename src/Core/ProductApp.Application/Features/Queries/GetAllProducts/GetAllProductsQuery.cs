using MediatR;
using ProductApp.Application.Features.Queries.GetProductById;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ServiceResponse<List<GetProductByIdViewModel>>>
    {

    }
}
