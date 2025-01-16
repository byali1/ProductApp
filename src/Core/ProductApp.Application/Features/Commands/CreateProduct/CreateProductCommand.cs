using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductApp.Application.Dto;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.CreateProduct
{
    public class CreateProductCommand : ProductDto, IRequest<ServiceResponse<Guid>> //Return tipi -> ServiceResponse<Guid>
    {
        //Parametreler
       
    }
}
