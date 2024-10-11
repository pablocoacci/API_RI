using Application.Jobs;
using Autofac;
using Core.Shared.Events;
using Core.Shared.Services;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;

namespace Application.Bootstrapping
{
    public class ApplicationModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ApplicationModule(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterApplicationServices(builder);
        }

        protected virtual void RegisterApplicationServices(ContainerBuilder builder)
        {
            RegisterDateTimeOffsetService(builder);
            RegisterSerilogLogger(builder);

            RegisterEventConsumers(builder);
            RegistrarJobExecutor(builder);
        }

        protected void RegisterDateTimeOffsetService(ContainerBuilder builder)
        {
            builder
                .RegisterType<DateTimeOffsetService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        protected void RegisterSerilogLogger(ContainerBuilder builder, string connectionString = null)
        {
            var sqlOptions = new MSSqlServerSinkOptions()
            {
                TableName = "Logs",
                AutoCreateSqlTable = true,
                SchemaName = "dbo",
            };

            var loggerConfig = new LoggerConfiguration()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.MSSqlServer(
                    string.IsNullOrEmpty(connectionString) ? _configuration["ConnectionStrings:Sql"] : connectionString,
                    sqlOptions
                );

            builder
                .Register<ILogger>(x => loggerConfig.CreateLogger())
                .SingleInstance();
        }

        protected void RegistrarJobExecutor(ContainerBuilder builder)
        {
            if (_configuration != null)
            {
                builder
                    .Register(JobConfigurationFactory)
                    .AsSelf()
                    .SingleInstance();
            }

            builder
                .RegisterType<RecurrentJobsExecutor>()
                .AsSelf()
                .SingleInstance();
        }

        protected void RegisterEventConsumers(ContainerBuilder builder)
        {
            var types = Assembly.Load("Application").GetTypes().Where(t => t.IsClass).ToList();
            var assignableTypeFrom = typeof(IEventConsumer<>);

            foreach (var t in types)
            {
                if (!assignableTypeFrom.IsAssignableFrom(t) && (!assignableTypeFrom.IsGenericTypeDefinition || !DoesTypeImplementOpenGeneric(t, assignableTypeFrom)))
                    continue;

                builder.RegisterType(t)
                    .As(t.FindInterfaces((type, criteria) =>
                    {
                        var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
                        return isMatch;
                    }, assignableTypeFrom))
                    .InstancePerLifetimeScope();
            }
        }

        protected virtual bool DoesTypeImplementOpenGeneric(Type type, Type openGeneric)
        {
            try
            {
                var genericTypeDefinition = openGeneric.GetGenericTypeDefinition();
                foreach (var implementedInterface in type.FindInterfaces((objType, objCriteria) => true, null))
                {
                    if (!implementedInterface.IsGenericType)
                        continue;

                    if (genericTypeDefinition.IsAssignableFrom(implementedInterface.GetGenericTypeDefinition()))
                        return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        private RecurrentJobConfiguration JobConfigurationFactory(IComponentContext ctx)
        {
            return new RecurrentJobConfiguration(
                Convert.ToInt32(_configuration["Hangfire:FrecuenciaTarea"])
                );

        }
    }
}
