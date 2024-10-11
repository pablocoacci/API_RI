using Application.Shared;

namespace Application.V1.Account.Post.Refresh.Models
{
    public class CanExtendSessionResultModel : ResponseBaseModel
    {
        public CanExtendSessionResultModel(bool isValid)
        {
            IsValid = isValid;
        }

        public bool IsValid { get; }
    }
}
