using Application.Shared.ErrorsCodesDefinitions;

namespace Application.Exceptions
{
    public class BusinessException : Exception
    {
        public ErrorCodes ErrorCode { get; private set; }

        public BusinessException(Exception innerException, ErrorCodes errorCode = ErrorCodes.NotDefined)
            : base(ErrorDiccionary.GetErrorDescription(errorCode), innerException)
        {
            ErrorCode = errorCode;
        }

        public BusinessException(ErrorCodes errorCode = ErrorCodes.NotDefined)
            : base(ErrorDiccionary.GetErrorDescription(errorCode))
        {
            ErrorCode = errorCode;
        }
    }
}
