using System;
using EventStore;
using Solyutor.CardFlow.Messages.BoardManagement;

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
        public virtual Guid Id { get; private set; }

        public AggregateRoot()
        {
            
        }

        protected void ApplyChange(BoardCreatedEvent @event)
        {
            //ApplyChange(@event, true);
        }
    }
}