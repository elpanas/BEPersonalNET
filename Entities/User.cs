namespace BEPersonal.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // Hashed
        public DateTime CreatedAt { get; set; }
        // Navigation properties can be added here if needed
        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
