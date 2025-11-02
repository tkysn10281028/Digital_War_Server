using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class UserLogin : BaseModel
    {
        public long UserId { get; set; }
        public string ClientKey { get; set; } = "";
    }
}
