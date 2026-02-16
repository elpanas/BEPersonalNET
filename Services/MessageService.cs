using AutoMapper;
using BEPersonal.DBs;
using BEPersonal.DTOs.In;
using BEPersonal.DTOs.Out;
using BEPersonal.Entities;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BEPersonal.Services
{
    public class MessageService : IMessageService
    {
        private PersonalDBContext _context;

        private readonly IMapper _mapper;

        public MessageService(IMapper mapper, PersonalDBContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Message> CreateMessage(MessageDTOIn message)
        {
            var messageEntity = _mapper.Map<Message>(message);
            await _context.AddAsync(messageEntity);
            await _context.SaveChangesAsync();
            return messageEntity;
        }

        public async Task<Message> GetMessageById(Guid id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
                throw new KeyNotFoundException($"Message with id {id} not found.");            

            return message;
        }

        public async Task<IEnumerable<MessageDTOOut>> GetAllMessages()
        {
            var messages = await _context.Messages.ToListAsync();
            return _mapper.Map<IEnumerable<MessageDTOOut>>(messages);
        }
    }
}
