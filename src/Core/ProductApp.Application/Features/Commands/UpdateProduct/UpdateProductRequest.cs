namespace ProductApp.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductRequest
    {
        public Guid Id { get; set; }
        public UpdateProductViewModel Product { get; set; }
    }
}
