using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Helpers
{
    public static class AppSettingsExtensions
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true) 
                .AddEnvironmentVariables()
                .Build();
        }

        public static Config.DBSettings GetApplicationConfiguration(string outputPath)
        {
            var configuration = new Config.DBSettings();

            var iConfig = GetIConfigurationRoot(outputPath);

            iConfig
                .GetSection("Db")
                .Bind(configuration);

            return configuration;
        }
    }
}
