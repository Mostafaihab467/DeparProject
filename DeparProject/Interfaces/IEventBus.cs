namespace DeparProject.Interfaces
{
    public interface IEventBus
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class;
        void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
    }
}
