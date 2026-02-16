using BEPersonal.Entities;

namespace BEPersonal.DTOs.In
{
    public class MessageDTOIn
    {
        public int UserId { get; set; }
        public required string Content { get; set; }
    }
}
