using System;

namespace Solyutor.CardFlow.Messages.BoardManagement
{
    public class ChangeBoardHandler
    {
        public int ExpectedVersion { get; set; }

        public Guid BoardId { get; set; }

        public string NewName { get; set; }

        public State[] NewStates { get; set; }
    }
}