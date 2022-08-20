using Newtonsoft.Json;
using SharedModels.Enum;

namespace SharedModels.Models
{
    public class ApiResponse<T>
    {
        public T Result { get; set; }
        public bool Succeeded { get; set; }
        public ErrorCodes StatusCode { get; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; }

        public ApiResponse(ErrorCodes statusCode, bool succeeded, T result, string message = null)
        {
            Succeeded = succeeded;
            StatusCode = statusCode;
            Result = result;
            Message = message;
        }

        public static ApiResponse<T> ApiFailResponse(ErrorCodes statusCode, string message)
        {
            return new ApiResponse<T>(statusCode, false, default(T), message);
        }

        public static ApiResponse<T> ApiOkResponse(T result)
        {
            return new ApiResponse<T>(ErrorCodes.VALID_REQUEST, true, result);
        }

    }
}
