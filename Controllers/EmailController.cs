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

        // ✅ POST endpoint
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest request)
        {
            if (string.IsNullOrEmpty(request.To) ||
                string.IsNullOrEmpty(request.Subject) ||
                string.IsNullOrEmpty(request.Body))
            {
                return BadRequest("Email, subject, and body are required.");
            }

            await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);

            return Ok("Email sent successfully via POST!");
        }

        // ✅ Simple test endpoint
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working on Railway!");
        }
    }

    // DTO for POST
    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
