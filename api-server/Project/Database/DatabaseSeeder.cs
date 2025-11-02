using ApiServer.Project.Database;
using ApiServer.Project.Domains;

namespace ApiServer.Project.Database
{
    public static class DatabaseSeeder
    {
        public static void Seed(AppDbContext db)
        {
            if (!db.Users.Any())
            {
                db.Users.AddRange(
                    new User { UserId = 1, DeviceId = "device1" }
                );
                db.UserDetails.AddRange(
                    new UserDetail { UserId = 1, Name = "Tak", ClientKey = "abc123" }
                );
                db.UserGuilds.AddRange(
                    new UserGuild { UserId = 1, GuildId = 1 }
                );
                db.UserGuildMasters.AddRange(
                    new UserGuildMaster { UserId = 1, GuildId = 1 }
                );
                db.UserLogins.AddRange(
                    new UserLogin { UserId = 1, ClientKey = "abc123" }
                );
                db.SaveChanges();
            }
        }
    }
}
