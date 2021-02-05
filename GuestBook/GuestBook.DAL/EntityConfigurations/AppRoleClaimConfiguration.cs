using GuestBook.BusinessObjects.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestBook.DAL.EntityConfigurations
{
    public class AppRoleClaimConfiguration : IEntityTypeConfiguration<AppRoleClaim>
    {
        public void Configure(EntityTypeBuilder<AppRoleClaim> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.ToTable("RoleClaims");
        }
    }
}