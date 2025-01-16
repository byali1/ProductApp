using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
