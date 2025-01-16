using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using ProductApp.Application.Wrappers;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductQuantityCommand : UpdateProductQuantityViewModel, IRequest<ServiceResponse<UpdateProductQuantityViewModel>>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
