using Microsoft.Extensions.DependencyInjection;

namespace Core.Shared.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventPublisher(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : EventArgs
        {
            var consumers = (IEnumerable<IEventConsumer<TEvent>>)_serviceProvider.GetServices(typeof(IEventConsumer<TEvent>));

            foreach (var consumer in consumers)
            {
                try
                {
                    //try to handle published event

                    consumer.HandleEvent(@event);
                }
                catch (Exception)
                {
                    //log error, we put in to nested try-catch to prevent possible cyclic (if some error occurs)
                    try
                    {
                        
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            if (!consumers.Any())
                throw new NotImplementedException();
        }

    }
}
