using System;

namespace Solyutor.CardFlow.Messages.BoardManagement
{
    public interface IEvent
    {
        Guid Id { get; set; }

        int Version { get; set; }
    }
}