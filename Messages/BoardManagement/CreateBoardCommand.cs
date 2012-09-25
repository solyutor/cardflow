namespace Solyutor.CardFlow.Messages.BoardManagement
{
    //All command should ends with command postfix.
    public class State
    {
        public string Name { get; set; }

        public byte Order { get; set; }

        public byte Capacity { get; set; }

    }

    public class CreateBoardCommand
    {
        public string Name { get; set; }

        public State[] States { get; set; }
    }
}