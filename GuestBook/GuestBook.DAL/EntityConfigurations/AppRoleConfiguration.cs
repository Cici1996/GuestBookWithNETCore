using GuestBook.BusinessObjects.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestBook.DAL.EntityConfigurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.Property(c => c.Description)
                .HasMaxLength(500);
            builder.ToTable("Roles");
        }
    }
}