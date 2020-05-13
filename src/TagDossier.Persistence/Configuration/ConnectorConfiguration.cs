using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagDossier.Domain.Entities;

namespace TagDossier.Persistence.Configuration
{
    public class ConnectorConfiguration : IEntityTypeConfiguration<Connector>
    {
        public void Configure(EntityTypeBuilder<Connector> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}