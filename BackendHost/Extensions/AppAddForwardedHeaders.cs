using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Extensions
{
    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AppAddForwardedHeaders(this IApplicationBuilder app)
        {
            var a = app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
                //, RequireHeaderSymmetry = false
                //, ForwardLimit = null
                //, KnownNetworks = { new IPNetwork(IPAddress.Parse("::ffff:172.17.0.1"), 104) }
            });

            return a;
        }
    }
}
