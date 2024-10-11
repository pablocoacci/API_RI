using Autofac;
using MediatR;
using Microsoft.Extensions.DependencyModel;
using System.Reflection;

namespace Application.Bootstraping
{
    public class MediatRModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterMediatR(builder);
            RegisterHandlers(builder);
        }

        private void RegisterMediatR(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        private void RegisterHandlers(ContainerBuilder builder)
        {
            var assemblies = DependencyContext.Default
                .GetDefaultAssemblyNames()
                .Select(x => Assembly.Load(x));

            // request handlers
            foreach (var assembly in assemblies)
            {
                RegisterHandlers(builder, assembly);
            }
        }

        private void RegisterHandlers(ContainerBuilder builder, Assembly assembly)
        {
            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Where(i => i.IsClosedTypeOf(typeof(IRequestHandler<>))).Any())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Where(i => i.IsClosedTypeOf(typeof(IRequestHandler<,>))).Any())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder
                .RegisterAssemblyTypes(assembly)
                .Where(t => t.GetInterfaces().Where(i => i.IsClosedTypeOf(typeof(INotificationHandler<>))).Any())
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }

    }
}
