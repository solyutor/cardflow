using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Solyutor.CardFlow.Backend.Domain
{
    public static class OnEventInvokeExtesions
    {
        private static readonly ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>> EventHandlers =
            new ConcurrentDictionary<Type, Dictionary<Type, MethodInfo>>();

        public static void InvokeOn(this AggregateRoot self, object @event)
        {
            Dictionary<Type, MethodInfo> methods;
            var aggregateType = self.GetType();
            EventHandlers.TryGetValue(aggregateType, out methods);

            if (methods == null)
            {
                methods = GetEventMethods(aggregateType);
                EventHandlers.TryAdd(aggregateType, methods);
            }

            methods[@event.GetType()].Invoke(self, new[] {@event});
        }

        private static Dictionary<Type, MethodInfo> GetEventMethods(Type aggregateType)
        {
            return aggregateType
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(x => x.Name.Equals("On"))
                .ToDictionary(x => x.GetParameters().Single().ParameterType, x => x);
        }
    }
}