using Microsoft.AspNetCore.Identity;
using System;

namespace GuestBook.BusinessObjects.Identities
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string PhotoLocation { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;

        public DateTime? UpdatedDate { get; set; } = DateTime.Now;

        public DateTime? DeletedDate { get; set; }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public string DeletedBy { get; set; }

        public bool IsActive { get; set; } = true;

        public bool IsDeleted { get; set; } = false;
        public bool IsNewAccount { get; set; } = true;

        public string Language { get; set; } = "en-US";
    }
}