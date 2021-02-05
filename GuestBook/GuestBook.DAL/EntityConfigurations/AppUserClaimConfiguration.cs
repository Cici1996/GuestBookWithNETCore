using GuestBook.BusinessObjects.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GuestBook.DAL.EntityConfigurations
{
    public class AppUserClaimConfiguration : IEntityTypeConfiguration<AppUserClaim>
    {
        public void Configure(EntityTypeBuilder<AppUserClaim> builder)
        {
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.ToTable("UserClaims");
        }
    }
}