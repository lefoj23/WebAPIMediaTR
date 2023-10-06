using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CQRSServices.Helpers
{
    public static class RegistrationRequestExtensions
    {
        public static bool isEmail(this string text)
        {
            var regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(text, regexPattern);
        }

        public static bool isContactNumber(this string text)
        {
            //only accepts philippine contactnumber which starts to "09" for example "09123456789"
            var regexPattern = @"^09\d{9}$";

            return Regex.IsMatch(text, regexPattern);
        }
        public static bool isAccIdentityValid(this string text)
        {
            return isEmail(text) || isContactNumber(text);
        }


        public static bool containsOneUppercase(this string password)
        {
            var regexPattern = @"[A-Z]";

            return Regex.IsMatch(password, regexPattern);
        }

        public static bool containsOneLowercase(this string password)
        {
            var regexPattern = @"[a-z]";

            return Regex.IsMatch(password, regexPattern);
        }

        public static bool containsDigit(this string password)
        {
            var regexPattern = @"\d";

            return Regex.IsMatch(password, regexPattern);
        }
    }
}
