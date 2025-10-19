using api_server.Project.Models;
using Microsoft.EntityFrameworkCore;

namespace api_server
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Test> Tests { get; set; }
    }
}
