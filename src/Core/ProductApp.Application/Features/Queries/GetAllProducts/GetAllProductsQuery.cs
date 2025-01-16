using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Features.Queries.GetProductById;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<ServiceResponse<List<GetProductByIdViewModel>>>
    {

    }
}
