using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagDossier.Domain.Entities;

namespace TagDossier.Persistence.Configuration
{
    public class SourceConfiguration : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Connector)
                .WithMany()
                .IsRequired();
            
            builder.ConfigureAuditInfo();
        }
    }
}