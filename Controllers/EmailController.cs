using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SendingMailsAPI.Services;

namespace SendingMailsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // ✅ GET endpoint with query parameters
        [HttpGet("send")]
        public async Task<IActionResult> SendEmail([FromQuery] string to, [FromQuery] string subject, [FromQuery] string body)
        {
            if (string.IsNullOrEmpty(to) || string.IsNullOrEmpty(subject) || string.IsNullOrEmpty(body))
            {
                return BadRequest("Email, subject, and body are required.");
            }

            await _emailService.SendEmailAsync(to, subject, body);

            return Ok("Email sent successfully via GET!");
        }

        // ✅ Simple test endpoint
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working on Railway!");
        }
    }
}
