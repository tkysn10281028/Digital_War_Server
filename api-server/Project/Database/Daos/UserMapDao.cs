using System.Threading.Tasks;
using ApiServer.Project.Common;
using ApiServer.Project.Domains;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Project.Database.Daos
{
    public class UserMapDao : IInjectable
    {
        private AppDbContext _context;
        public UserMapDao(AppDbContext context) => _context = context;
        public async Task<List<UserMap>> FindByGuildId(long guildId)
        {
            return await _context.UserMaps.Where(u => u.GuildId == guildId).ToListAsync();
        }

        public async Task Insert(List<UserMap> userMapList)
        {
            foreach (var userMap in userMapList)
            {
                _context.UserMaps.Add(userMap);
            }
            await _context.SaveChangesAsync();
        }
    }
}