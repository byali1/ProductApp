using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public UpdateProductViewModel Product { get; set; }
    }
}
