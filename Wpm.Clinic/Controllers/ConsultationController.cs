using Microsoft.AspNetCore.Mvc;

namespace Wpm.Consultation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController : ControllerBase
    {
         
        private readonly ILogger<ConsultationController> _logger;

        public ConsultationController(ILogger<ConsultationController> logger)
        {
            _logger = logger;
        }

        [HttpPost("/Start")]
        public async Task<IActionResult> Start(StartConsultationCommand _startConsultationCommand)
        {
            return Ok();
        }
    }

    public record StartConsultationCommand(int PatientId);
}