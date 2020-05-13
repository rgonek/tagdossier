using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagDossier.Domain.Entities;

namespace TagDossier.Persistence.Configuration
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExternalId)
                .HasMaxLength(2000)
                .IsRequired();

            builder.HasOne(x => x.Source)
                .WithMany()
                .IsRequired();

            builder.HasMany(x => x.Dossiers)
                .WithOne(x => x.Resource)
                .IsRequired();

            builder.ConfigureAuditInfo();
        }
    }
}