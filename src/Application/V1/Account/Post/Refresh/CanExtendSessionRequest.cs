using Application.Shared;
using Application.V1.Account.Post.Refresh.Models;

namespace Application.V1.Account.Post.Refresh
{
    public class CanExtendSessionRequest : LoggedRequest<CanExtendSessionResultModel>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
