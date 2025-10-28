using ApiServer.Project.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiServer
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Test> Tests { get; set; }
    }
}
