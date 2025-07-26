using Infotecs_WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infotecs_WebApi.Infrastructure.Configurations
{
    internal sealed class ValueConfiguration : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            builder.ToTable("Values");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.ValueDefinition).IsRequired().HasColumnName("Value");
            builder.Property(x => x.ExecutionTime).IsRequired();
            builder.Property(x => x.Date)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        }
    }
}
