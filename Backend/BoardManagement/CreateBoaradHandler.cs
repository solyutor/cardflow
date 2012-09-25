using System;
using Rhino.ServiceBus;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.BoardManagement
{
    public class CreateBoaradHandler : ConsumerOf<CreateBoardCommand>
    {
        public void Consume(CreateBoardCommand message)
        {
            Console.WriteLine("ReceivedMessage!");
        }
    }
}