using static ApiServer.AppDbContext;

namespace ApiServer.Project.Models
{
    public class UserDetail : BaseModel
    {
        public long UserId { get; set; }
        public string Name { get; set; } = "";
        public string ClientKey { get; set; } = "";
    }
}
