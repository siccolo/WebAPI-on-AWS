using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public sealed class RedeemLogDbEntry
    {
        [Key]
        public DateTime Created { get; set; }
        public string RedeemCode { get; set; }

        public string RedeemResult { get; set; }

        public string CallerIP { get; set; }
        public string CallerUserAgent { get; set; }

        public RedeemLogDbEntry()
        {

        }

        public RedeemLogDbEntry(Models.RedeemCode code, Models.RedeemResult resultCodeRedeem, Models.CallerInfo caller)
        {
            Created = System.DateTime.Now;
            RedeemCode = code.Value;
            RedeemResult = !resultCodeRedeem.Success ? Models.RedeemResultEnum.Error.Value : resultCodeRedeem.Result.Value;
            CallerIP = caller.IP;
            CallerUserAgent = caller.UserAgent;
        }
    }
}
