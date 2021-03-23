namespace API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Debug")]
        public IActionResult Debug()
        {
            _logger.LogDebug("Debugging...");
            return Ok();
        }

        [HttpGet("Info")]
        public IActionResult Information()
        {
            _logger.LogInformation("Log some info");
            return Ok();
        }

        [HttpGet("Warn")]
        public IActionResult Warning()
        {
            _logger.LogWarning("Log some warning");
            return Ok();
        }

        [HttpGet("Error")]
        public IActionResult Error()
        {
            _logger.LogError("Log some Error");
            return Ok();
        }

        [HttpGet("Critical")]
        public IActionResult Critical()
        {
            _logger.LogCritical("Log some Critical");
            return Ok();
        }
    }
}