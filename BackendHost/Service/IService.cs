using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public interface IService<Tin, Tout>
    {
        Task<Tout> Process(Tin ins);
    }
}
