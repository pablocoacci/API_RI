namespace Core.Shared.Services.Mailing
{
    public class MailModel<T> where T : class
    {
        public MailModel(string subject, T data)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public string Subject { get; set; }

        public T Data { get; set; }
    }

    public static class MailModel
    {
        public static MailModel<T> Create<T>(string subject, T data) where T : class
        {
            return new MailModel<T>(subject, data);
        }
    }
}
