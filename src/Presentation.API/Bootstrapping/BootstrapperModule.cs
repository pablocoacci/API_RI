using Application.Bootstrapping;
using Autofac;

namespace Presentation.API.Bootstraping
{
    public class BootstrapperModule : Module
    {
        private readonly IConfiguration configuration;

        public BootstrapperModule(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new CoreModule(configuration));
            builder.RegisterModule(new MediatRModule());
            builder.RegisterModule(new DataModule());
            builder.RegisterModule(new MailingModule(configuration));
            builder.RegisterModule(new ApplicationModule(configuration));
            builder.RegisterModule(new Application.Bootstraping.MediatRModule());
            base.Load(builder);
        }
    }
}
