using Autofac;
using Core.Shared.Configuration;
using Core.Shared.Services;
using Serilog;
using Serilog.Events;

namespace Presentation.API.Bootstraping
{
    public class CoreModule : Autofac.Module
    {
        private readonly IConfiguration configuration;

        public CoreModule(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterFrontendOptions(builder);
            RegisterDateTimeOffsetService(builder);
            RegisterSerilogLogger(builder);
        }

        private void RegisterFrontendOptions(ContainerBuilder builder)
        {
            builder
                .RegisterInstance(configuration
                    .GetSection(nameof(FrontendOptions))
                    .Get<FrontendOptions>(x => x.BindNonPublicProperties = true))
                .AsSelf()
                .SingleInstance();

            builder
                .Register(FrontendOptionsFactory)
                .AsSelf()
                .SingleInstance();
        }

        private FrontendOptions FrontendOptionsFactory(IComponentContext ctx)
        {
            var config = ctx.Resolve<IConfiguration>();
            return new FrontendOptions(
                config["FrontendOptions:Url"],
                config["FrontendOptions:ConfirmAccount"],
                config["FrontendOptions:ForgotPassword"]);
        }

        private void RegisterDateTimeOffsetService(ContainerBuilder builder)
        {
            builder
                .RegisterType<DateTimeOffsetService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        private void RegisterSerilogLogger(ContainerBuilder builder)
        {
            var loggerConfig = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.File(
                    configuration["Logging:LogPath"],
                    rollingInterval: Enum.Parse<RollingInterval>(configuration["Logging:LogRollingInterval"]),
                    restrictedToMinimumLevel: Enum.Parse<LogEventLevel>(configuration["Logging:LogLevel"]),
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] [MachineName: {MachineName}] [EnviromentUserName: {EnvironmentUserName}] {Message}{NewLine}{Exception}"
                );

            builder.Register<Serilog.ILogger>(x => loggerConfig.CreateLogger()).SingleInstance();
        }
    }
}
