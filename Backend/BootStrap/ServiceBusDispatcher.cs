using System.Linq;
using EventStore;
using EventStore.Dispatcher;
using Rhino.ServiceBus;

namespace Solyutor.CardFlow.Backend.BootStrap
{
    public class ServiceBusDispatcher : DelegateMessageDispatcher
    {
        private readonly IServiceBus _bus;

        public ServiceBusDispatcher(IServiceBus bus)
            : base(delegate { })
        {
            _bus = bus;
        }

        public override void Dispatch(Commit commit)
        {
            object[] events = commit.Events.Select(x => x.Body).ToArray();

            _bus.Notify(events);
        }
    }
}