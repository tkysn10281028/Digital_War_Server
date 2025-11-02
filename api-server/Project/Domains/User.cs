using static ApiServer.Project.Database.AppDbContext;

namespace ApiServer.Project.Domains
{
    public class User : BaseModel
    {
        public long UserId { get; set; }
        public string DeviceId { get; set; } = "";
    }
}
