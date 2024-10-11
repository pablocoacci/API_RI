using Application.Shared;
using Core.Shared.Services.Mailing;
using Core.V1.Account.Post.Reset.Models;

namespace Core.V1.Account.Post.Reset
{
    public class ResetPasswordRequest : LoggedRequest<EmptyResponse>
    {
        private string _baseUrl = "";

        public string Email { get; set; }
        
        private Func<MailModel<ResetPasswordEmailDataModel>, Task<string>> emailBodyBuilder;

        public void SetEmailBodyBuilder(Func<MailModel<ResetPasswordEmailDataModel>, Task<string>> func)
        {
            emailBodyBuilder = func;
        }

        public Task<string> GetEmailBody(MailModel<ResetPasswordEmailDataModel> mailModel)
        {
            return emailBodyBuilder(mailModel);
        }

        public void SetBaseUrl(string baseurl)
            => _baseUrl = baseurl;

        public string GetBaseUrl()
            => _baseUrl;
    }
}
