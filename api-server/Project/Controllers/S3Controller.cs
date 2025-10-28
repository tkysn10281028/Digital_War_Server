using ApiServer.Project.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiServer.Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class S3Controller(S3Service s3Service) : ControllerBase
    {
        private readonly S3Service _s3Service = s3Service;

        [HttpGet("files")]
        public async Task<IActionResult> GetFiles()
        {
            var files = await _s3Service.GetFileListAsync();
            return Ok(files);
        }
    }
}
