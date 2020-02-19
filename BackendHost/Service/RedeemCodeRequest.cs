using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public sealed class RedeemCodeRequest
    {
        public Models.RedeemCode RedeemCode { get; private set; }
        public Models.CallerInfo CallerInfo { get; private set; }

        public RedeemCodeRequest(Models.RedeemCode code, Models.CallerInfo caller)
        {
            RedeemCode = code;
            CallerInfo = caller;
        }
    }
}
