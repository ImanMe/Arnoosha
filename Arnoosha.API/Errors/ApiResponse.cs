using Arnoosha.API.Enums;

namespace Arnoosha.API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GenerateDefaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GenerateDefaultMessageForStatusCode(in int statusCode)
        {
            return statusCode switch
            {
                400 => ErrorMessage.BadRequest,
                401 => ErrorMessage.NotAuthorized,
                404 => ErrorMessage.NotFound,
                500 => ErrorMessage.InternalServerError,
                _ => null
            };
        }
    }
}
