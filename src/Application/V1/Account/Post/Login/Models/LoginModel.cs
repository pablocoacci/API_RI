using Application.Shared;
using System.Collections.Immutable;
using System.Security.Claims;

namespace Application.V1.Account.Post.Login.Models
{
    public class LoginModel : ResponseBaseModel
    {
        public LoginModel() { }

        public LoginModel(string id, IImmutableList<Claim> claims)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Claims = claims ?? throw new ArgumentNullException(nameof(claims));
        }

        public string Id { get; set; }
        public IImmutableList<Claim> Claims { get; internal set; }
    }
}
