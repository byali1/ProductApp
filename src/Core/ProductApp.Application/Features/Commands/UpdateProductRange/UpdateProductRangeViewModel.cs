using ProductApp.Application.Dto;

namespace ProductApp.Application.Features.Commands.UpdateProductRange
{
    public class UpdateProductRangeViewModel : ProductDto
    {
        public Guid Id { get; set; }
    }
}
