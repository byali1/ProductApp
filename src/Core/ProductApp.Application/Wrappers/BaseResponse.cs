namespace ProductApp.Application.Wrappers
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
    }
}
