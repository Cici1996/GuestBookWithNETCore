using System;
using System.Collections.Generic;
using System.Text;

namespace GuestBook.Core.Constants
{
    public class ModelMessageConstants
    {
        public const string UsernameRequired = "Username is Required";
        public const string PasswordRequired = "Password is Required";
        public const string MaxLength255Chars = "Max length 255";
        public const string MinLength4Chars = "Min length 4";
        public const string UserInactive = "User Inactive";
        public const string AccountLockoutMessage = "Account Locked for {0}";
        public const string UserNameOrPasswordIsInvalid = "Username or Password Is Invalid";
    }
}
