using Autofac;
using Autofac.Extensions.DependencyInjection;
using Presentation.API.Bootstraping;

namespace Presentation.API.Helpers.Extensions
{
    public static class HostBuilderExtension
    {
        public static void AutofacConfiguration(this IHostBuilder host, IConfiguration configuration)
        {
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new BootstrapperModule(configuration)));
        }
    }
}
