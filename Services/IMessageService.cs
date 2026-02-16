using BEPersonal.DTOs.In;
using BEPersonal.DTOs.Out;
using BEPersonal.Entities;

namespace BEPersonal.Services
{
    public interface IMessageService
    {
        Task<Message> CreateMessage(MessageDTOIn message);

        Task<Message> GetMessageById(Guid id);
        Task<IEnumerable<MessageDTOOut>> GetAllMessages();
    }
}
