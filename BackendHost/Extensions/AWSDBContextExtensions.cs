using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Microsoft.EntityFrameworkCore;


namespace Extensions
{
    public static partial class ServiceCollectionExtensions 
    {
        public static IServiceCollection AddAWSDBContext(this IServiceCollection services, IConfiguration configuration, string configurationSectionName)
        {
            services.Configure<Config.DBSettings>(configuration.GetSection(configurationSectionName));
            var sp = services.BuildServiceProvider();
            var dboptions = sp.GetService<IOptions<Config.DBSettings>>();
            string connectioninfo = dboptions.Value.ConnectionInfo;
            var s = services.AddDbContext<Datastore.RCDBContext>(
                        options =>
                        {
                            options.UseMySql(connectioninfo);
                        }
                        , ServiceLifetime.Transient);
            return s;
        }

    }
}
