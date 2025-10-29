using Microsoft.AspNetCore.Mvc;
using Shared.ApiEndPoints;

namespace ApiServer.Project.Base
{
    [ApiController]
    public abstract class ApiServerControllerBase<TEndpoint, TRequest, TResponse> : ControllerBase
        where TEndpoint : EndPointBase<TRequest, TResponse>, new()
        where TRequest : class, new()
        where TResponse : class, new()
    {
        protected readonly TEndpoint _endpoint = new();
        protected abstract Task<TResponse> HandleCoreAsync(TRequest request);

        [NonAction]
        public async Task<ActionResult> ExecuteAsync(TRequest request)
        {
            try
            {
                var response = await HandleCoreAsync(request);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"[ERROR] {ex}");
                return StatusCode(400, new { error = "Bad Request" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

    }
}