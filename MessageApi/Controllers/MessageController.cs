using MessageApi.Data;
using MessageApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MessageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly MessageDbContext _context;
        public MessageController(MessageDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessageById(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return NotFound();
            return message;
        }
        [HttpPost]
        public async Task<ActionResult<Message>> CreateMessage (Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMessageById", new { id = message.MessageId }, message);
        }

    }
}
