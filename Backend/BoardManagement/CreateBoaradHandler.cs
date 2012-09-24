using Messages.BoardManagement;
using Rhino.ServiceBus;

namespace Solyutor.CardFlow.Backend.BoardManagement
{
    public class CreateBoaradHandler : ConsumerOf<CreateBoardCommand>
    {
        public void Consume(CreateBoardCommand message)
        {
            throw new System.NotImplementedException();
        }
    }
}