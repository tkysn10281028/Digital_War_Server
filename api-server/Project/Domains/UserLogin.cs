using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class UserLogin : BaseModel
    {
        public long UserId { get; set; }
        public string ClientKey { get; set; } = "";
        public UserLogin(long userId, string clientKey)
        {
            UserId = userId;
            ClientKey = clientKey;
        }
    }
}
