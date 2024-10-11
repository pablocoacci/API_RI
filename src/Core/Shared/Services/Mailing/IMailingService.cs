namespace Core.Shared.Services.Mailing
{
    public interface IMailingService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
