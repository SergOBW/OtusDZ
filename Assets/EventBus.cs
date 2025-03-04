using System;
using System.Collections.Generic;

namespace ShootEmUp
{
    public class EventBus
    {
        public delegate void EventHandler<in T>(T eventData);
        private Dictionary<Type, Delegate> _handlers = new Dictionary<Type, Delegate>();

        public void Subscribe<T>(EventHandler<T> handler)
        {
            if (_handlers.TryGetValue(typeof(T), out var existing))
                _handlers[typeof(T)] = Delegate.Combine(existing, handler);
            else
                _handlers[typeof(T)] = handler;
        }

        public void Unsubscribe<T>(EventHandler<T> handler)
        {
            if (_handlers.TryGetValue(typeof(T), out var existing))
                _handlers[typeof(T)] = Delegate.Remove(existing, handler);
        }

        public void Publish<T>(T eventData)
        {
            if (_handlers.TryGetValue(typeof(T), out var handler))
                (handler as EventHandler<T>)?.Invoke(eventData);
        }
    }

// Событие для смерти игрока
    public class PlayerDiedEvent { }
}