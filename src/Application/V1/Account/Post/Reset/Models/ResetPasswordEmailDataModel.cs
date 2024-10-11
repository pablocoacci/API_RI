namespace Core.V1.Account.Post.Reset.Models
{
    public class ResetPasswordEmailDataModel
    {
        public ResetPasswordEmailDataModel(string token, string forgotPasswordUrl)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));
            ForgotPasswordUrl = forgotPasswordUrl;
        }

        public string Token { get; set; }
        public string ForgotPasswordUrl { get; }
    }
}
