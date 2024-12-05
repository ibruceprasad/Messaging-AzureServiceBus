 
using MessageSender.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MessageSender.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingMessageController : ControllerBase
    {
        
        private readonly ILogger<GreetingMessageController> _logger;
        private readonly IBusServices _busServices;
        private const string QueueName = "messaging_servicebus_queue";

        public GreetingMessageController(ILogger<GreetingMessageController> logger, IBusServices busServices)
        {
            _busServices = busServices;
            _logger = logger;
        }

        [HttpPost("send",Name = "push")]
        public async Task<IActionResult> SendMessage([FromBody] GreetingMessage message)
        {
            await _busServices.SendMessageAsync(message);
            return Ok();
        }
    }
}
