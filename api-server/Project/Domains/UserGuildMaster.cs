using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class UserGuildMaster : BaseModel
    {
        public long UserId { get; set; }
        public long GuildId { get; set; }
    }
}
