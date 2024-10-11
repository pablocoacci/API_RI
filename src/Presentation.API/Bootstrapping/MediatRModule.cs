using Autofac;
using MediatR;
using Presentation.API.Helpers;

namespace Presentation.API.Bootstraping
{
    public class MediatRModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterTransactionalBehavior(builder);
        }

        private void RegisterTransactionalBehavior(ContainerBuilder builder)
        {
            builder
                .RegisterGeneric(typeof(TransactionalBehavior<,>))
                .As(typeof(IPipelineBehavior<,>))
                .InstancePerLifetimeScope();
        }
    }
}
