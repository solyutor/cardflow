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
            _events.Add(events);
        }

        protected void Apply(object @event, bool isNew = true)
        {
            if (isNew)
            {
                this.InvokeOn(@event);
            }
            _events.Add(@event);
        }
    }
}