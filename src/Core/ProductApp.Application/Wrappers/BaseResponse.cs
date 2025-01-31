namespace ProductApp.Application.Wrappers
{
    public class BaseResponse
    {
        public string RequestId { get; set; } 
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public int StatusCode { get; set; } = 200;
    }
}
