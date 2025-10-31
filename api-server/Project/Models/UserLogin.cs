using static ApiServer.AppDbContext;

namespace ApiServer.Project.Models
{
    public class UserLogin : BaseModel
    {
        public long UserId { get; set; }
        public string ClientKey { get; set; } = "";
    }
}
