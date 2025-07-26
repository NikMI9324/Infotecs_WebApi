using Infotecs_WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infotecs_WebApi.Infrastructure.Configurations
{
    internal sealed class ResultConfiguration : IEntityTypeConfiguration<Result>
    {
        public void Configure(EntityTypeBuilder<Result> builder)
        {
            builder.ToTable("Results");
            builder.HasKey(x => x.FileName);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.MinimalDate)
                .HasColumnType("timestamp with time zone")
                .HasConversion(
                    v => v.ToUniversalTime(),
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        }
    }
}
