namespace Core.Services.Mailing
{
    public class MailingServiceOptions
    {
        public MailingServiceOptions(MailingServiceSmtpOptions smtp, MailingServiceFromOptions from)
        {
            Smtp = smtp ?? throw new ArgumentNullException(nameof(smtp));
            From = from ?? throw new ArgumentNullException(nameof(from));
        }

        public MailingServiceSmtpOptions Smtp { get; }
        public MailingServiceFromOptions From { get; }
    }

    public class MailingServiceSmtpOptions
    {
        public MailingServiceSmtpOptions(string host, int port, string username, string password)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Port = port;
            Username = username;
            Password = password;
        }

        public string Host { get; }
        public int Port { get; }
        public string Username { get; }
        public string Password { get; }
    }

    public class MailingServiceFromOptions
    {
        public MailingServiceFromOptions(string name, string address)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }

        public string Name { get; }
        public string Address { get; }
    }
}
