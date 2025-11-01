using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class UserMap : BaseModel
    {
        public long GuildId { get; set; }
        public string MapName { get; set; } = string.Empty;
    }
}
