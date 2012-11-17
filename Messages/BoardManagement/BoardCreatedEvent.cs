using System;

namespace Solyutor.CardFlow.Messages.BoardManagement
{
    public class BoardCreatedEvent
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }

        public State[] States { get; set; }
    }
}