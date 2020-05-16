using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagDossier.Domain.Entities;
using TagDossier.Domain.ValueObjects;

namespace TagDossier.Persistence.Configuration
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(1000)
                .IsRequired();

            builder.HasOne(x => x.Parent)
                .WithMany();

            builder.OwnsOne(x => x.Color, b =>
            {
                b.Property(x => x.Text)
                    .HasColumnName(nameof(Color.Text) + nameof(Color))
                    .HasColumnType("varchar(6)")
                    .IsRequired();
                b.Property(x => x.Background)
                    .HasColumnName(nameof(Color.Background) + nameof(Color))
                    .HasColumnType("varchar(6)")
                    .IsRequired();
            });
            
            builder.ConfigureAuditInfo();
        }
    }
}