using GuestBook.BusinessObjects.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestBook.DAL.EntityConfigurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(c => c.FullName)
                .HasMaxLength(200);
            builder.Property(c => c.PhotoLocation)
                .HasMaxLength(2000);
            builder.ToTable("Users");
        }
    }
}