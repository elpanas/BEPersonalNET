namespace BEPersonal.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
