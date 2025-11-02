using ApiServer.Project.Domains.Logics;
using Shared.ApiEndPoints;

namespace ApiServer.Project.Services
{
    public abstract class ApiServerBaseService<TRequest, TResponse>
        where TRequest : RequestBase, new()
        where TResponse : ResponseBase, new()
    {
        private UserGuildLogic _userGuildLogic;
        public ApiServerBaseService(UserGuildLogic userGuildLogic)
        {
            _userGuildLogic = userGuildLogic;
        }
        protected abstract Task<TResponse> HandleCoreAsync(TRequest request);

        public async Task<TResponse> ExecuteAsync(TRequest request)
        {
            if (request.GuildId == 0 || request.UserId == 0)
            {
                throw new ArgumentException("Id Is Required.");
            }

            var userGuild = await _userGuildLogic.FindById(request.UserId, request.GuildId);
            if (userGuild == null)
            {
                throw new ArgumentException("User Guild not Found.");
            }
            return await HandleCoreAsync(request);
        }
    }
}