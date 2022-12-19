
using PasswordGenerator;

namespace Configuration.Core.Utilities.Security.PasswordCreator
{
    public class PasswordHelper
    {
        public static string CreatePassword()
        {
            var pwd = new Password(includeLowercase: true, includeUppercase: true, includeNumeric: true, includeSpecial: true,
                passwordLength: 10);
            return pwd.Next();
        }
    }
}
