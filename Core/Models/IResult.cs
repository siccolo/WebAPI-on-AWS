using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public interface IResult<T>
    {
        bool Success { get; }
        T Result { get; }

        System.Exception Exception { get; }

        string AdditionalInfo { get; }

    }
}
