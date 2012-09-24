using EventStore;

namespace Solyutor.CardFlow.Backend
{
    public class Board : AggregateRoot
    {
        public Board(string name)
        {
            //On(new BoardCreated)
        } 
    }

    public class AggregateRoot
    {
        //protected void ApplyChange(Event @event)
        //{
        //    //ApplyChange(@event, true);
        //}
    }
}