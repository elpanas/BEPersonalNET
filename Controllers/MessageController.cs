using AutoMapper;
using BEPersonal.DTOs.In;
using BEPersonal.DTOs.Out;
using BEPersonal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BEPersonal.Controllers
{
    [ApiController]
    [Route("api/message")]
    [Produces("application/json")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMessage([FromBody] MessageDTOIn message)
        {
            try
            {
                var messageEntity = await _messageService.CreateMessage(message);

                // mappo l'entità creata in DTO di output
                var dtoOut = _mapper.Map<MessageDTOOut>(messageEntity);

                // ritorno 201 + URL della risorsa
                return CreatedAtAction(nameof(GetMessageById), new { id = messageEntity.Id }, dtoOut);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessageById(Guid id)
        {
            var message = await _messageService.GetMessageById(id);
            if (message == null)
                return NotFound();
            var dtoOut = _mapper.Map<MessageDTOOut>(message);
            return Ok(dtoOut);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessages();
            if (messages == null || messages.Any())
                return NotFound();

            return Ok(messages);
        }
    }
}
