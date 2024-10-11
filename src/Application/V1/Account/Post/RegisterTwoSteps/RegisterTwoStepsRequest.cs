using Application.Shared;
using Application.V1.Account.Post.RegisterTwoSteps.Models;
using Core.Shared.Services.Mailing;

namespace Application.V1.Account.Post.RegisterTwoSteps
{
    public class RegisterTwoStepsRequest : LoggedRequest<EmptyResponse>
    {
        private string _baseUrl = "";
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PasswordRepeat { get; set; }
        public override bool SaveChanges()
            => true;

        private Func<MailModel<EmailVerificationModelData>, Task<string>> emailBodyBuilder;

        public void SetEmailBodyBuilder(Func<MailModel<EmailVerificationModelData>, Task<string>> func)
        {
            emailBodyBuilder = func;
        }

        public Task<string> GetEmailBody(MailModel<EmailVerificationModelData> mailModel)
        {
            return emailBodyBuilder(mailModel);
        }

        public void SetBaseUrl(string baseurl)
            => _baseUrl = baseurl;

        public string GetBaseUrl()
            => _baseUrl;
    }
}
