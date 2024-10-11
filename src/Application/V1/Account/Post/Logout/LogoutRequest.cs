using Application.Shared;

namespace Application.V1.Account.Post.Logout
{
    public class LogoutRequest : LoggedRequest<EmptyResponse>
    {
        public LogoutRequest(string userId, string accessToken)
        {
            UserId = userId;
            AccessToken = accessToken;
        }

        public string UserId { get; }
        public string AccessToken { get; }
        public override bool SaveChanges()
            => true;
    }
}
