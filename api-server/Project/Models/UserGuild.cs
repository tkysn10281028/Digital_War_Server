using static ApiServer.AppDbContext;

namespace ApiServer.Project.Models
{
    public class UserGuild : BaseModel
    {
        public long UserId { get; set; }
        public long GuildId { get; set; }
    }
}
