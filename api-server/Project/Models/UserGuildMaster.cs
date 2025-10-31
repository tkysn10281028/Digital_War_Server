using static ApiServer.AppDbContext;

namespace ApiServer.Project.Models
{
    public class UserGuildMaster : BaseModel
    {
        public long UserId { get; set; }
        public long GuildId { get; set; }
    }
}
