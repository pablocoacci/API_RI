using System.Net;
using System.Net.Mail;

namespace Core.Services.Mailing
{
    public interface ISmtpClientFactory
    {
        SmtpClient CreateSmptClient(string userName, string password, string host, int port);
    }

    public class SmtpClientFactory : ISmtpClientFactory
    {
        public SmtpClient CreateSmptClient(string userName, string password, string host, int port)
        {
            NetworkCredential loginInfo = new NetworkCredential(userName, password);

            var smtpClient = new SmtpClient(host, port);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;

            return smtpClient;
        }
    }
}
