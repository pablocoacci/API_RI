namespace Core.Shared.Events
{
    public interface IEventConsumer<T>
    {
        void HandleEvent(T @event);
    }
}
