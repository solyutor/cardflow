using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solyutor.CardFlow.Backend.Domain
{
    public class AggregateRoot
    {
        private readonly IList _events;

        public AggregateRoot()
        {
            _events = new ArrayList();
        }

        public IEnumerable Events
        {
            get { return _events; }
        }

        public virtual Guid Id { get; protected set; }

        public virtual void ApplyEvents(IEnumerable events)
        {
            foreach (var @event in events)
            {
                AddEvent(@event, false);
            }
        }

        protected void AddEvent(object @event, bool isNew = true)
        {
            if (isNew)
            {
                this.InvokeOnEvent(@event);
            }

            _events.Add(@event);
        }
    }

    public static class ReflectionExtensions
    {
        private static ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>> _methodsCache;

        public static void InvokeOnEvent(this AggregateRoot aggregateRoot, object @event)
        {
            var aggregateType = aggregateRoot.GetType();

            var method = GetMethod(@event, aggregateType);

            method.Invoke(aggregateRoot, new[] {@event});
        }

        private static MethodInfo GetMethod(object @event, Type aggregateType)
        {
            _methodsCache = new ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>>();

            var methods = _methodsCache.GetOrAdd(aggregateType, GetEventMethods);

            MethodInfo method;

            methods.TryGetValue(@event.GetType(), out method);

            if (method == null)
            {
                throw new InvalidOperationException(string.Format("Cannot find method 'On({0})' for type {1}", @event.GetType(), aggregateType));
            }

            return method;
        }

        private static Dictionary<Type, MethodInfo> GetEventMethods(Type aggregateType)
        {
            return aggregateType
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "On")
                .ToDictionary(x => x.GetParameters().Single().ParameterType, x => x);
        }
    }
}