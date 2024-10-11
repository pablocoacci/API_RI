namespace Presentation.API.Helpers.Models
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel(int errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
