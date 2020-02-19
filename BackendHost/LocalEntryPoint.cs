using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
//
//using Core.Configuration;
using Microsoft.Extensions.Configuration;

namespace BackendHost
{
    /// <summary>
    /// The Main function can be used to run the ASP.NET Core application locally using the Kestrel webserver.
    /// </summary>
    public class LocalEntryPoint
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //  //moved to Startup
                /*
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddRcbAffiliateSettings(context.HostingEnvironment.EnvironmentName, args);
                })
                */
                //
                .UseStartup<Startup>()
                .Build();
    }
}
