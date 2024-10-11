using Autofac;
using Core.Shared.Services.Mailing;
using Microsoft.Extensions.Configuration;

namespace Presentation.API.Bootstraping
{
    public class MailingModule : Module
    {
        private readonly IConfiguration configuration;

        public MailingModule(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(MailingServiceOptionsFactory)
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<SmtpClientFactory>()
                .As<ISmtpClientFactory>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<MailingService>()
                .As<IMailingService>()
                .InstancePerLifetimeScope();
        }

        private MailingServiceOptions MailingServiceOptionsFactory(IComponentContext ctx)
        {
            return new MailingServiceOptions(
                new MailingServiceSmtpOptions(
                    configuration["MailingServiceOptions:Smtp:Host"],
                    int.Parse(configuration["MailingServiceOptions:Smtp:Port"]),
                    configuration["MailingServiceOptions:Smtp:Username"],
                    configuration["MailingServiceOptions:Smtp:Password"]),
                new MailingServiceFromOptions(
                    configuration["MailingServiceOptions:From:Name"],
                    configuration["MailingServiceOptions:From:Address"]));
        }
    }
}
