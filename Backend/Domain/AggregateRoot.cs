using System;
using System.Collections;

namespace Solyutor.CardFlow.Backend.Domain
{
    public class AggregateRoot
    {
        private readonly IList _uncommittedEvents;

        public AggregateRoot()
        {
            _uncommittedEvents = new ArrayList();
        }

        public IEnumerable UncommittedEvents
        {
            get { return _uncommittedEvents; }
        }

        public virtual Guid Id { get; protected set; }

        public virtual int Version { get; private set; }

        public virtual void ApplyEvents(IEnumerable events)
        {
            foreach (object @event in events)
            {
                AddEvent(@event, false);
            }
        }

        protected void AddEvent(object @event, bool isNew = true)
        {
            this.InvokeOnEvent(@event);
            
            Version++;

            if (isNew)
            {
                _uncommittedEvents.Add(@event);
            }
        }
    }
}