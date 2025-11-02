using System.Threading.Tasks;
using ApiServer.Project.Common;
using ApiServer.Project.Database.Daos;

namespace ApiServer.Project.Domains.Logics
{
    public class UserMapLogic : IInjectable
    {
        private UserMapDao _dao;
        public UserMapLogic(UserMapDao dao) => _dao = dao;
        public async Task<List<UserMap>> FindByGuildId(long guildId)
        {
            return await _dao.FindByGuildId(guildId);
        }
        public async Task Insert(long guildId, List<string> mapNameList)
        {
            var userMapList = mapNameList.Select(u =>
            {
                return new UserMap(guildId, u);
            }).ToList();
            await _dao.Insert(userMapList);
        }
    }
}