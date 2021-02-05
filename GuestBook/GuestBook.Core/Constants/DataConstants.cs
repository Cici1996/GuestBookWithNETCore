namespace GuestBook.Core.Constants
{
    public class AuthenticationConstants
    {
        public const string DefaultPassword = "default";
    }

    public class RoleListConstants
    {
        public const string Administrator = "Administrator";
        public const string CommonUser = "Normal User";
    }

    public class CustomClaimNames
    {
        public const string Name = "CLAIM_NAME";
        public const string Email = "CLAIM_EMAIL";
        public const string Phone = "CLAIM_PHONE";
        public const string UserName = "CLAIM_USERNAME";
        public const string Id = "CLAIM_ID";
        public const string Language = "CLAIM_LANGUAGE";
    }

    public class IdentityBasedConstants
    {
        public const string IdClaim = "sub";
        public const string FullNameClaim = "name";
        public const string EmailClaim = "email";
        public const string UserNameClaim = "username";
        public const string RoleClaim = "role";
    }
}