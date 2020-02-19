using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public sealed class vwRedeemCode
    {
        public DateTime Created { get; private set; }

        [Key]
        public string RedeemCode { get; private set; }

        public string RedeemResult { get; private set; }

        public vwRedeemCode()
        {

        }

        public vwRedeemCode(Models.RedeemCode code, Models.RedeemResult result)
        {
            Created = System.DateTime.Now;
            RedeemCode = code.Value;
            RedeemResult = !result.Success ? Models.RedeemResultEnum.Error.Value : result.Result.Value;
        }
    }
}
