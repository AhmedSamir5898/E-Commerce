namespace Talabat.Apis.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public ApiResponse ( int statusCode , string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetMessageByStatusCode(statusCode);
        }

        private string? GetMessageByStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad Request , you are made",
                401 => "Authorized , You are not",
                404 => "Not Found",
                _ => null
            };
        }
    }
}
