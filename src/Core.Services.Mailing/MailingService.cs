using Core.Shared.Services.Mailing;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Core.Services.Mailing
{
    public class MailingService : IMailingService
    {
        private SmtpClient _smtpClient;
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly MailingServiceOptions _options;

        public MailingService(
            ISmtpClientFactory smtpClientFactory,
            MailingServiceOptions options)
        {
            _smtpClientFactory = smtpClientFactory;
            _options = options ?? throw new ArgumentNullException(nameof(options));

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

            _smtpClient = _smtpClientFactory.CreateSmptClient(options.Smtp.Username, options.Smtp.Password, options.Smtp.Host, options.Smtp.Port);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(_options.Smtp.Username, _options.From.Name);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = htmlMessage;
            msg.To.Add(email);
            _smtpClient.Send(msg);
            await Task.CompletedTask;
        }
    }
}
