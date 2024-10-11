using Application.Shared;
using Application.V1.Account.Post.Login.Models;

namespace Application.V1.Account.Post.Login
{
    public class LoginRequest : LoggedRequest<LoginModel>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
