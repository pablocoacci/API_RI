using FluentValidation.Results;

namespace Presentation.API.Helpers.Models
{
    public class ErrorBadRequestModel
    {
        public ErrorBadRequestModel()
        {
            IsValid = true;
            Errors=new List<ErrorDetail>();
        }

        public ErrorBadRequestModel(ValidationResult result)
        {
            IsValid = result.IsValid;
            Errors = result.Errors.Select(x => new ErrorDetail()
            {
                ErrorCode = x.ErrorCode,
                ErrorMessage = x.ErrorMessage,
                PropertyName = x.PropertyName,
            });
        }

        public bool IsValid { get; private set; }
        public IEnumerable<ErrorDetail> Errors { get; private set; }

    }

    public class ErrorDetail
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }
}
