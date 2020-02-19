using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

using Microsoft.AspNetCore.Builder;

namespace Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            var s = services.AddSwaggerGen(c =>
            {
                ///index.html
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BackendAPI", Version = "v1" });
            });

            return s;
        }
    }

    public static partial class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddSwaggerServices(this IApplicationBuilder app)
        {
            var a = app.UseSwagger()
                    .UseSwaggerUI(c =>
                    {
                        //https://localhost:44354/index.html
                        c.RoutePrefix = String.Empty;// "swagger/ui";
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackendAPI(v1)");
                    });

            return a;
        }
    }
}
