using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public sealed class RedeemResultEnum
    {
        private RedeemResultEnum(string value) { Value = value; }

        public string Value { get; set; }

        public static RedeemResultEnum Success => new RedeemResultEnum("success");
        public static RedeemResultEnum AlreadyRedeemed => new RedeemResultEnum("already redeemed");
        public static RedeemResultEnum InvalidCode => new RedeemResultEnum("invalid code");

        public static RedeemResultEnum Error => new RedeemResultEnum("error");

        public static RedeemResultEnum NotFound => new RedeemResultEnum("not found");

        public static RedeemResultEnum FromString(string value)
        {
            return
                value.Equals(RedeemResultEnum.Success.Value, StringComparison.InvariantCultureIgnoreCase) ? RedeemResultEnum.Success :
                    value.Equals(RedeemResultEnum.AlreadyRedeemed.Value, StringComparison.InvariantCultureIgnoreCase) ? RedeemResultEnum.AlreadyRedeemed :
                    value.Equals(RedeemResultEnum.NotFound.Value, StringComparison.InvariantCultureIgnoreCase) ? RedeemResultEnum.NotFound :
                        RedeemResultEnum.InvalidCode;
        }
    }

    [System.Runtime.Serialization.DataContract]
    public sealed class RedeemResult : IResult<RedeemResultEnum>
    {
        //Replies with JSON for success, already redeemed, invalid code
        [System.Runtime.Serialization.DataMember]
        public bool Success { get; private set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public RedeemResultEnum Result { get; private set; }

        [System.Runtime.Serialization.DataMember]
        public string ResultValue { get { return !Success ? RedeemResultEnum.Error.Value : Result.Value; } }

        [System.Runtime.Serialization.DataMember]
        public string AdditionalInfo { get; private set; } = "";

        [System.Text.Json.Serialization.JsonIgnore]
        public System.Exception Exception { get; private set; }

        //  ctors:
        public RedeemResult(System.Exception exception, string info = "")
        {
            Success = false;
            //Exception = exception;
            Exception = new System.Exception(exception.Message);
            AdditionalInfo = info;
        }

        public RedeemResult(RedeemResultEnum result, string info="")
        {
            Success = true;
            Result = result;
            AdditionalInfo = info;
        }

        public RedeemResult(string result, string info="")
        {
            Success = true;
            Result = RedeemResultEnum.FromString(result);
            AdditionalInfo = info;
        }
        //

        public string ToJSON()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(this);
            return json;
        }
    }
}
