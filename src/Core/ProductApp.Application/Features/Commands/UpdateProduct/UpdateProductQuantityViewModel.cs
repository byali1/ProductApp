using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductQuantityViewModel
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
