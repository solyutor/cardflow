using System;
using System.Collections;

namespace Solyutor.CardFlow.Backend.Domain
{
    public class AggregateRoot
    {
        private readonly IList _events;

        public AggregateRoot()
        {
            _events = new ArrayList();
        }

        public IEnumerable Events
        {
            get { return _events; }
        }

        public virtual Guid Id { get; protected set; }

        public virtual void ApplyEvents(IEnumerable events)
        {
            dynamic thisAsDynamic = this;
            foreach (object @event in events)
            {
                thisAsDynamic.On(@event);
            }
        }

        protected void AddEvent(object @event)
        {
            _events.Add(@event);
        }
    }
}