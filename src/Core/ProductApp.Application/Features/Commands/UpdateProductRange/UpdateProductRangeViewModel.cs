using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductApp.Application.Dto;

namespace ProductApp.Application.Features.Commands.UpdateProductRange
{
    public class UpdateProductRangeViewModel : ProductDto
    {
        public Guid Id { get; set; }
    }
}
