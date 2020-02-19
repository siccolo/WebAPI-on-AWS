using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Configuration
{
    //moved to BackendHost.Config.DBSettings
    /*
    public static class ConfigurationHelpers
    {

        public static void AddRcbAffiliateSettings(this IConfigurationBuilder config, string envName, string[] args)
        {
            if (envName.Equals("Local"))
            {
                string runtimeversion = Helpers.AssemblyInfo.RuntimeVersion();//"netcoreapp2.2"|"netcoreapp2.1"
                var pathToGlobalConfigFiles = Path.Combine("bin", "Debug", runtimeversion);

                config.AddJsonFile(
                    Path.Combine(pathToGlobalConfigFiles, "globalsettings.json"),
                    optional: true);

                config.AddJsonFile(
                    Path.Combine(pathToGlobalConfigFiles, $"globalsettings.{envName}.json"),
                    optional: true);
            }

            config.AddJsonFile(
                Path.Combine("globalsettings.json"),
                optional: true);

            config.AddJsonFile(
                Path.Combine($"globalsettings.{envName}.json"),
                optional: true);

            config.AddEnvironmentVariables();

            if (args != null)
            {
                config.AddCommandLine(args);
            }
        }

         
        //public static void AddRcbAffiliateOptions(this IServiceCollection services, IConfiguration config)
        //{
        //    services.AddOptions();
        //    services.Configure<RcbAffiliateOptions>(config.GetSection("RcbAffiliate"));
        //}
         
        //  more generic, may want to change to interface...
        public static void AddOptions<TClientOptions>(this IServiceCollection services, IConfiguration config, string configurationSectionName) where TClientOptions : Core.Configuration.DataStoreOptions
        {
            services.AddOptions();
            services.Configure<TClientOptions>(config.GetSection(configurationSectionName));
        }
    }
    */
}
