using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public sealed class BoolResult : IResult<bool>
    {
        //Replies with JSON for success, already redeemed, invalid code
        public bool Success { get; private set; }
        public bool Result { get; private set; }

        public string AdditionalInfo { get; private set; } = "";

        public System.Exception Exception { get; private set; }

        public BoolResult(System.Exception exception, string info = "")
        {
            Success = false;
            //Exception = exception;
            Exception = new System.Exception(exception.Message);
            AdditionalInfo = info;
        }

        public BoolResult(bool result, string info = "")
        {
            Success = true;
            Result = result;
            AdditionalInfo = info;
        }
    }
}
