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
            var entityId = Guid.NewGuid();

            var boardCreatedEvent = new BoardCreatedEvent
                                        {
                                            Id = entityId, 
                                            Version = 1, 
                                            Name = message.Name, 
                                            States = message.States
                                        };

            using (var stream = _storeEvents.CreateStream(entityId))
            {
                stream.Add(new EventMessage{Body = boardCreatedEvent});
                stream.CommitChanges(Guid.NewGuid());
            }
            
            _bus.Notify(boardCreatedEvent);
        }
    }
}