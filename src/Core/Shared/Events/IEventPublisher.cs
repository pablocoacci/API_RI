namespace Core.Shared.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : EventArgs;
    }
}
