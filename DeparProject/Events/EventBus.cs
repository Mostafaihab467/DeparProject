using DeparProject.Interfaces;

namespace DeparProject.Events
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Action<object>>> _handlers = new();
        //  private readonly Dictionary<Type, List<Action<List<Action<object>>>>> _handlers = new();
        public void Publish<TEvent>(TEvent @event) where TEvent : class
        {
            var eventType = typeof(TEvent);

            if (_handlers.ContainsKey(eventType))
            {
                foreach (var handler in _handlers[eventType])
                {
                    //      var events = new List<Action<object>> { @event as dynamic, @event as dynamic };
                    // handler(@events.ForEach(e=>e.Invoke(@event )));
                    handler.Invoke(@event);
                }
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
        {
            var eventType = typeof(TEvent);
            if (!_handlers.ContainsKey(eventType))
            {
                _handlers[eventType] = new List<Action<object>>();
            }
            _handlers[eventType].Add(e => handler((TEvent)e));
        }
    }
}
