using System;
using Rhino.ServiceBus;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.BoardManagement
{
    public class CreateBoaradHandler : ConsumerOf<CreateBoardCommand>
    {
        private readonly IServiceBus _bus;

        public CreateBoaradHandler(IServiceBus bus)
        {
            _bus = bus;
        }

        public void Consume(CreateBoardCommand message)
        {
            _bus.Publish(new BoardCreatedEvent());
        }
    }
}