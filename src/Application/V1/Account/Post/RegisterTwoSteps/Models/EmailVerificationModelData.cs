namespace Application.V1.Account.Post.RegisterTwoSteps.Models
{
    public class EmailVerificationModelData
    {
        public EmailVerificationModelData(string token, string email, string emailVerificationUrl)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            EmailVerificationUrl = emailVerificationUrl ?? throw new ArgumentNullException(nameof(emailVerificationUrl));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public string Token { get; set; }
        public string Email { get; set; }
        public string EmailVerificationUrl { get; }
    }
}
