using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class UserDetail : BaseModel
    {
        public long UserId { get; set; }
        public string Name { get; set; } = "";
        public string ClientKey { get; set; } = "";
    }
}
