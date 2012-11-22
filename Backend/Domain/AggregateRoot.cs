using System;
using System.Collections;
using System.Collections.Generic;
using Solyutor.CardFlow.Messages.BoardManagement;

namespace Solyutor.CardFlow.Backend.Domain
{
    public class AggregateRoot
    {
        private readonly IList _uncommittedEvents;

        public AggregateRoot() 
        {
            _uncommittedEvents = new ArrayList();
            Id = Guid.NewGuid();
        }

        internal void SetId(Guid id)
        {
            Id = id;
        }

        public IEnumerable UncommittedEvents
        {
            get { return _uncommittedEvents; }
        }

        public virtual Guid Id { get; private set; }

        public virtual int Version { get; private set; }

        public virtual void ApplyEvents(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                AddEvent(@event, false);
            }
        }

        protected void AddEvent(IEvent @event, bool isNew = true)
        {
            this.InvokeOnEvent(@event);

            Version++;

            @event.Id = Id;
            @event.Version = Version;

            if (isNew)
            {
                _uncommittedEvents.Add(@event);
            }
        }
    }
}