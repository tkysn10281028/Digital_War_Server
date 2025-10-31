using static ApiServer.AppDbContext;

namespace ApiServer.Project.Models
{
    public class User : BaseModel
    {
        public long UserId { get; set; }
        public string DeviceId { get; set; } = "";
    }
}
