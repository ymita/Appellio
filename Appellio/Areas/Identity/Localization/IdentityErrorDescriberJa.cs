using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Appellio.Areas.Identity.Localization
{
    public class IdentityErrorDescriberJa : IdentityErrorDescriber
    {
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError {
                Code = nameof(InvalidEmail),
                Description = $"Email '{email}' は無効な形式です。"
            };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "パスワードは少なくとも 1 つの英数字以外の文字が必要です。"
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError {
                Code = nameof(PasswordRequiresLower),
                Description = "パスワードは少なくとも 1 つの小文字 ('a'-'z') が必要です。"
            };
        }

        public override IdentityError PasswordRequiresUpper() {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "パスワードは少なくとも 1 つの大文字 ('A'-'Z') が必要です。"
            };
        }
    }
}
