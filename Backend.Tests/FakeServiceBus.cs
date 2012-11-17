using System;
using System.Collections.Generic;
using System.Linq;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Messages;

namespace Solyutor.CardFlow.Backend.Tests
{
    public class Holder
    {
        private readonly List<object> _all;

        public Holder()
        {
            _all = new List<Object>();
        }

        public List<object> All
        {
            get { return _all; }
        }

        public TResult Last<TResult>()
        {
            return (TResult) _all.Last();
        }
    }

    public class FakeServiceBus : IServiceBus
    {
        public FakeServiceBus()
        {
            Notified = new Holder();
            Published = new Holder();
        }

        public Holder Published { get; private set; }
        public Holder Notified { get; private set; }

        public void Publish(params object[] messages)
        {
            Published.All.AddRange(messages);
        }

        public void Notify(params object[] messages)
        {
            Notified.All.AddRange(messages);
        }

        public void Reply(params object[] messages)
        {
            throw new NotImplementedException();
        }

        public void Send(Endpoint endpoint, params object[] messages)
        {
            throw new NotImplementedException();
        }

        public void Send(params object[] messages)
        {
            throw new NotImplementedException();
        }

        public void ConsumeMessages(params object[] messages)
        {
            throw new NotImplementedException();
        }

        public IDisposable AddInstanceSubscription(IMessageConsumer consumer)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T>()
        {
            throw new NotImplementedException();
        }

        public void Subscribe(Type type)
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe<T>()
        {
            throw new NotImplementedException();
        }

        public void Unsubscribe(Type type)
        {
            throw new NotImplementedException();
        }

        public void DelaySend(Endpoint endpoint, DateTime time, params object[] msgs)
        {
            throw new NotImplementedException();
        }

        public void DelaySend(DateTime time, params object[] msgs)
        {
            throw new NotImplementedException();
        }

        public Endpoint Endpoint { get; set; }
        public CurrentMessageInformation CurrentMessageInformation { get; set; }

        public event Action<Reroute> ReroutedEndpoint;
    }
}