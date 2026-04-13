using System;
using System.Collections.Generic;
using UnityEngine;

namespace TrapMaster
{
    public class EventBus 
    {
        private readonly Dictionary<Type, List<Delegate>> eventHandlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var type = typeof(TEvent);

            if (!eventHandlers.TryGetValue(type, out var handlers))
            {
                handlers = new List<Delegate>();
                eventHandlers[type] = handlers;
            }

            if (!handlers.Contains(handler))
                handlers.Add(handler);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            var type = typeof(TEvent);

            if (!eventHandlers.TryGetValue(type, out var handlers))
                return;

            handlers.Remove(handler);

            if (handlers.Count == 0)
                eventHandlers.Remove(type);
        }

        public void Publish<TEvent>(TEvent eventData)
        {
            if (eventData == null)
            {
                Debug.LogWarning($"EventBus: попытка опубликовать null событие ({typeof(TEvent).Name}) проигнорирована.");
                return;
            }

            var type = typeof(TEvent);

            if (!eventHandlers.TryGetValue(type, out var handlers) || handlers.Count == 0)
                return;

            var handlersCopy = new List<Delegate>(handlers);

            foreach (var handler in handlersCopy)
            {
                try
                {
                    ((Action<TEvent>)handler)?.Invoke(eventData);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"EventBus: ошибка в обработчике события {type.Name}: {ex}");
                }
            }
        }

    }
    public class MonsterDie
    {
        public Vector3 DiePosition;
        public bool isX3;

        public MonsterDie(Vector3 DiePosition, bool isX3)
        {
            this.DiePosition = DiePosition;
            this.isX3 = isX3;
        }

    }
    public class TrapAdded
    {
        public int newLevel;
        public int newPrice;

        public TrapAdded(int newLevel, int newPrice)
        {
            this.newLevel = newLevel;
            this.newPrice = newPrice;
        }

    }
}