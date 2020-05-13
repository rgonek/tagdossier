using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagDossier.Domain.Entities;

namespace TagDossier.Persistence.Configuration
{
    public class DossierConfiguration : IEntityTypeConfiguration<Dossier>
    {
        public void Configure(EntityTypeBuilder<Dossier> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Resource)
                .WithMany(x => x.Dossiers)
                .IsRequired();

            builder.HasOne(x => x.Tag)
                .WithMany()
                .IsRequired();

            builder.ConfigureAuditInfo();
        }
    }
}