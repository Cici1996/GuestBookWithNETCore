using Microsoft.AspNetCore.Identity;
using System;

namespace GuestBook.BusinessObjects.Identities
{
    public class AppUserClaim: IdentityUserClaim<Guid>
    {
    }
}
