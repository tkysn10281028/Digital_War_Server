using System.Threading.Tasks;
using ApiServer.Project.Common;
using ApiServer.Project.Database.Daos;

namespace ApiServer.Project.Domains.Logics
{
    public class UserGuildLogic : IInjectable
    {
        private UserGuildDao _dao;
        public UserGuildLogic(UserGuildDao dao) => _dao = dao;
        public async Task<UserGuild?> FindById(long userId, long guildId)
        {
            return await _dao.FindById(userId, guildId);
        }
    }
}