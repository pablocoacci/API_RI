using Application.Shared;

namespace Application.V1.Account.Post.ChangePassword
{
    public class ChangePasswordRequest : LoggedRequest<EmptyResponse>
    {
        public string Token { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordRepeat { get; set; } = string.Empty;

        public override bool SaveChanges()
            => true;
    }
}
