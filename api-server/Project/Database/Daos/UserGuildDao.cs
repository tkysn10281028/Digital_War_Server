using System.Threading.Tasks;
using ApiServer.Project.Common;
using ApiServer.Project.Domains;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Project.Database.Daos
{
    public class UserGuildDao : IInjectable
    {
        private AppDbContext _context;
        public UserGuildDao(AppDbContext context) => _context = context;
        public async Task<UserGuild?> FindById(long userId, long guildId)
        {
            return await _context.UserGuilds
                .FirstOrDefaultAsync(u => u.UserId == userId && u.GuildId == guildId);
        }
    }
}