using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace DataModels
{
    public sealed class RedeemCodeDbEntry
    {
        public DateTime Created { get; private set; }

        [Key]
        public string RedeemCode { get; private set; }

        public RedeemCodeDbEntry()
        {

        }

        public RedeemCodeDbEntry(Models.RedeemCode code)
        {
            Created = System.DateTime.Now;
            RedeemCode = code.Value;
        }
    }
}
