using Microsoft.AspNetCore.Identity;

namespace Biblioteka.Areas.Identity.Data
{
    public class BibErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError { Code = "PasswordRequiresDigitsError", Description = "Hasło musi zawierać przynajmniej jedną cyfrę." };
        }

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = "LoginAlreadyTakenError", Description = "Konto z podanym emailem już istnieje." };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError { Code = "asd", Description = "asdasdasd." };
        }
    }
}
