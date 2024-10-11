using Application.Shared;

namespace Application.V1.Account.Post.Register
{
    public class RegisterRequest : LoggedRequest<EmptyResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PasswordRepeat { get; set; } = string.Empty;
        public override bool SaveChanges()
            => true;
    }
}
