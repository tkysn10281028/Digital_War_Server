using ApiServer.Project.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.ApiEndPoints;

namespace ApiServer.Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class S3Controller : ApiServerControllerBase<S3GetMapNameList, S3GetMapNameList.Request, S3GetMapNameList.Response>
    {
        private readonly S3Service _s3Service;
        public S3Controller(S3Service s3Service)
        {
            _s3Service = s3Service;
        }

        [HttpPost("getMapNameList")]
        public async Task<ActionResult<S3GetMapNameList.Response>> GetMapNameList([FromBody] S3GetMapNameList.Request request)
        {
            return await ExecuteAsync(request);
        }
        protected override async Task<S3GetMapNameList.Response> HandleCoreAsync(S3GetMapNameList.Request request)
        {
            var files = await _s3Service.GetMapNameListAsync(request);
            return await _s3Service.GetMapNameListAsync(request);
        }
    }
}
