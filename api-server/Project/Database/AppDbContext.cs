using ApiServer.Project.Common;
using ApiServer.Project.Domains;
using Microsoft.EntityFrameworkCore;

namespace ApiServer.Project.Database
{
    public class AppDbContext : DbContext, IInjectable
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Test> Tests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserGuild> UserGuilds { get; set; }
        public DbSet<UserGuildMaster> UserGuildMasters { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserMap> UserMaps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Test>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedAt)
                      .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<UserDetail>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<UserGuild>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GuildId });
            });

            modelBuilder.Entity<UserGuildMaster>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.GuildId });
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<UserMap>(entity =>
            {
                entity.HasKey(e => new { e.GuildId, e.MapName });
            });
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseModel entity)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedAt = now;
                        entity.UpdatedAt = now;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entity.UpdatedAt = now;
                    }
                }
            }
        }

        public abstract class BaseModel
        {
            public long CreatedAt { get; set; }
            public long UpdatedAt { get; set; }
        }
    }
}
