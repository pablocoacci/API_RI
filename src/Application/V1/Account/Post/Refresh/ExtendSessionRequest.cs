using Application.Shared;

namespace Application.V1.Account.Post.Refresh
{
    public class ExtendSessionRequest : LoggedRequest<EmptyResponse>
    {
        public ExtendSessionRequest(string userId, string oldRefreshToken, string accessToken, TimeSpan validFor)
        {
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            OldRefreshToken = oldRefreshToken ?? throw new ArgumentNullException(nameof(oldRefreshToken));
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            ValidFor = validFor;
        }

        public string UserId { get; }
        public string OldRefreshToken { get; }
        public string AccessToken { get; }
        public TimeSpan ValidFor { get; }
        public override bool SaveChanges()
            => true;
    }
}
