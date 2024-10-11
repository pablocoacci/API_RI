using Application.Shared.ErrorsCodesDefinitions;

namespace Application.Shared
{
    public class ResponseBaseModel
    {
        private ErrorCodes _errorCode = ErrorCodes.OK;
        private bool _saveChanges = false;

        public bool IsError()
            => _errorCode != ErrorCodes.OK;

        public ErrorCodes GetErrorCode()
            => _errorCode;

        public void SetErrorCode(ErrorCodes code)
            => _errorCode = code;

        public bool GetSaveChanges()
            => _saveChanges;

        public void SetSaveChanges(bool saveChanges = true)
            => _saveChanges = saveChanges;
    }
}
