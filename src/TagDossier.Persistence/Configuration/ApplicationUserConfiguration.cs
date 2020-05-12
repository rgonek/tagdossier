using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TagDossier.Domain.Entities;

namespace TagDossier.Persistence.Configuration
{
    public class ApplicationUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(256)
                .IsRequired();
        }
    }
}