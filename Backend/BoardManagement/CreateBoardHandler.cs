using System;
using EventStore;
using Rhino.ServiceBus;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.BoardManagement
{
    public class CreateBoardHandler : ConsumerOf<CreateBoardCommand>
    {
        private readonly IServiceBus _bus;
        private readonly IStoreEvents _storeEvents;

        public CreateBoardHandler(IServiceBus bus, IStoreEvents storeEvents)
        {
            _bus = bus;
            _storeEvents = storeEvents;
        }

        public void Consume(CreateBoardCommand message)
        {
            var guid = Guid.NewGuid();
            using(var stream = _storeEvents.CreateStream(guid))
            {
                stream.Add(new EventMessage());
                _bus.Publish(new BoardCreatedEvent());
                stream.CommitChanges(Guid.NewGuid());
            }
            
        }
    }
}