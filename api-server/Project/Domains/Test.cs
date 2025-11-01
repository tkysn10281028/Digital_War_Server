namespace ApiServer.Project.Domains
{
    public class Test
    {
        public long Id { get; set; }
        public long CreatedAt { get; set; } = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    }
}
