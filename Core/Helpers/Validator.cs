using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Helpers
{
    public static class Validator
    {
        public static bool IsAlpaNumeric(this string s)
        {
            //System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[a-zA-Z0-9]*$");
            bool valid = s.All(c => char.IsLetterOrDigit(c));   //|| char.IsWhiteSpace(x));
            return valid;
        }
    }
}
