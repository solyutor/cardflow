namespace Messages.BoardManagement
{
    public class BoardCreatedEvent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int Version { get; set; }
    }
}