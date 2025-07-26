using Infotecs_WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infotecs_WebApi.Infrastructure.Data
{
    public class InfotecsDbContext : DbContext
    {
        public InfotecsDbContext(DbContextOptions<InfotecsDbContext> options) : base(options)
        {
        }
        public DbSet<Result> Results { get; set; }
        public DbSet<Value> Values { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
