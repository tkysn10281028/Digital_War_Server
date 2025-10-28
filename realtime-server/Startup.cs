using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace RealtimeServer
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var connectUrl = Environment.GetEnvironmentVariable("REDIS_HOST") != null ? "redis:6379" : "localhost:6379";
            var redis = ConnectionMultiplexer.Connect(connectUrl);
            services.AddSingleton<IConnectionMultiplexer>(redis);
            services.AddMagicOnion().UseRedisGroup(options =>
                {
                    options.ConnectionMultiplexer = redis;
                }, registerAsDefault: true);
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapMagicOnionService();
            });
        }
    }
}
