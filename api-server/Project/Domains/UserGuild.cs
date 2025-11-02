using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class UserGuild : BaseModel
    {
        public long UserId { get; set; }
        public long GuildId { get; set; }
        public UserGuild(long userId, long guildId)
        {
            UserId = userId;
            GuildId = guildId;
        }
    }
}
