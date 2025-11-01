using System.Threading.Tasks;
using ApiServer.Project.Common;
using ApiServer.Project.Database.Daos;

namespace ApiServer.Project.Domains.Logics
{
    public class UserMapLogic : IInjectable
    {
        private UserMapDao _dao;
        public UserMapLogic(UserMapDao dao) => _dao = dao;
        public async Task<UserMap?> FindById(long guildId)
        {
            return await _dao.FindById(guildId);
        }
    }
}