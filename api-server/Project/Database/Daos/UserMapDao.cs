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
        public async Task<UserMap?> FindById(long guildId)
        {
            return await _context.UserMaps
                .FirstOrDefaultAsync(u => u.GuildId == guildId);
        }
    }
}