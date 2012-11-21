using System;
using System.Collections.Generic;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.Domain
{
    public class Board : AggregateRoot
    {
        private readonly List<State> _states;
        
        public string Name { get; protected set; }

        public IEnumerable<State> States
        {
            get { return _states; }
        }
        
        public Board(Guid id, string name, State[] states)
        {
            _states = new List<State>();

            var @event = new BoardCreatedEvent
                             {
                                 Id = id,
                                 Version = 1,
                                 Name = name,
                                 States = states
                             };

            AddEvent(@event);
        }

        private void On(BoardCreatedEvent @event)
        {
            Id = @event.Id;
            _states.AddRange(@event.States);
            Name = @event.Name;
        }
    }
}