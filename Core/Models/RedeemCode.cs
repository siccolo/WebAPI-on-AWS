using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpers;

namespace Models
{
    public sealed class RedeemCode
    {
        public string Value { get; private set; }
        public RedeemCode(string code)
        {
            if (!RedeemCode.IsValid(code))
            {
                throw new System.ArgumentException("code");
            }
            Value = code;
        }

        public static bool IsValid(string code)
        {
            //Alpha numeric. Treat it as an exact string match. Support 64 chars, but we only generated 9 char codes
            bool valid = code.IsAlpaNumeric() && code.Length == 9;
            return valid;
        }
    }
}
