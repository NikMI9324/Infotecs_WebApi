using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infotecs_WebApi.Infrastructure.Data
{
    public class InfotecsDbContextFactory : IDesignTimeDbContextFactory<InfotecsDbContext>
    {
        public InfotecsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InfotecsDbContext>();
            //var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Infotecs_WebApi.Web");

            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            //if (string.IsNullOrWhiteSpace(connectionString))
            //{
            //    var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../Infotecs_WebApi.Web");

            //    var config = new ConfigurationBuilder()
            //        .SetBasePath(basePath)
            //        .AddJsonFile("appsettings.json", optional: true)
            //        .Build();

            //    connectionString = config.GetConnectionString("DefaultConnection");
            //}
            optionsBuilder.UseNpgsql(connectionString,
                x => x.MigrationsAssembly("Infotecs_WebApi.Infrastructure"));

            return new InfotecsDbContext(optionsBuilder.Options);
        }
    }
}
