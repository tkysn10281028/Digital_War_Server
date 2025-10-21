using api_server.Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_server.Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("status")]
        public IActionResult GetHealthStatus()
        {
            return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
        }

        [HttpGet("db")]
        public IActionResult GetDbStatus()
        {
            var canConnect = _context.Database.CanConnect();
            if (!_context.Database.CanConnect())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    status = "Unreachable",
                    message = "Database connection failed",
                    timestamp = DateTime.UtcNow
                });
            }
            var test = new Test();
            _context.Tests.Add(test);
            _context.SaveChanges();

            var latest = _context.Tests.OrderByDescending(t => t.Id).FirstOrDefault();
            return Ok(new { status = "Healthy", latest });
        }
    }
}
