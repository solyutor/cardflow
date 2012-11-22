using System;
using EventStore;
using Rhino.ServiceBus;
using Solyutor.CardFlow.Backend.Domain;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.BoardManagement
{
    public class CreateBoardHandler : ConsumerOf<CreateBoardCommand>
    {
        private readonly IStoreEvents _storeEvents;

        public CreateBoardHandler(IStoreEvents storeEvents)
        {
            _storeEvents = storeEvents;
        }

        public void Consume(CreateBoardCommand message)
        {
            var entityId = Guid.NewGuid();

            var board = new Board(entityId, message.Name, message.States);

            using (var stream = _storeEvents.CreateStream(entityId))
            {
                foreach (var @event in board.UncommittedEvents)
                {
                    stream.Add(new EventMessage {Body = @event});
                }
                stream.CommitChanges(Guid.NewGuid());
            }
        }
    }
}