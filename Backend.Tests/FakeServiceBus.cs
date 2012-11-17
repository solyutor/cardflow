using System;
using Castle.MicroKernel;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Rhino.ServiceBus.Internal;
using Rhino.ServiceBus.Messages;

namespace Solyutor.CardFlow.Backend.Tests
{
    
    public class FakeServiceBus : IServiceBus
    {
        private readonly IKernel _kernel;
        private readonly IOnewayBus _replyBus;

        public FakeServiceBus(IKernel kernel, IOnewayBus replyBus)
        {
            _kernel = kernel;
            _replyBus = replyBus;
        }

        public void Publish(params object[] messages)
        {
            throw new NotImplementedException();
        }

        public void Notify(params object[] messages)
        {
            throw new NotImplementedException();
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
        private ConsumerOf<TMessage>[] GetConsumers<TMessage>()
        {
            return _kernel.ResolveAll<ConsumerOf<TMessage>>();
        }

        public Endpoint Endpoint { get; private set; }
        public CurrentMessageInformation CurrentMessageInformation { get; private set; }
        public event Action<Reroute> ReroutedEndpoint;
    }
}