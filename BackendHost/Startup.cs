using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Extensions;

namespace BackendHost
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";
        //private readonly Core.Configuration.DataStoreOptions _dboptions;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //_dboptions = configuration.GetSection("RcbAffiliate").Get<Core.Configuration.DataStoreOptions>();
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHttpContextAccessor();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //  
            //services.AddOptions<Core.Configuration.DataStoreOptions>(Configuration, "RcbAffiliate");   //see ...\Core\Extensions\ConfigurationHelpers.cs  //moved to AddAWSDBContext()
            services.AddRedeemCodeServices();

            /*
            services.AddDbContext<RcbAffiliateContext>(
                    o => o.UseMySql(_options.Db.GetWriteConnectionString()), ServiceLifetime.Transient);
                    */
            services.AddAWSDBContext(Configuration, "Db");
            //
            services.AddSwaggerServices();

            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //  --- allow all
            app.UseCors(builder =>
                         builder.WithOrigins(
                                        "https://verizonup.redcross.us")
                                .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials());
            //

            app.AppAddForwardedHeaders();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.AddAppErrorHandling();
                #region swagger
                app.AddSwaggerServices();
                #endregion
            }
            else
            {
                app.AddAppErrorHandling();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();           
        }
    }
}
