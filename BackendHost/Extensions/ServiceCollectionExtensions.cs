using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedeemCodeServices(this IServiceCollection services)
        {
            var s = services
                    //.AddTransient(typeof(Service.IService<Models.RedeemCode, Models.RedeemResult>), typeof(Service.RedeemService));
                    .AddTransient(typeof(Service.IService<Service.RedeemCodeRequest, Models.RedeemResult>), typeof(Service.RedeemService));

            return s;
        }
    }
}
