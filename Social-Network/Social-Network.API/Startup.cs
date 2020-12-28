using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Social_Network.API.Infrastructure.Config;
using Social_Network.API.Infrastructure.Manages.HubConnect;

namespace Social_Network.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.SetInterfaceDi(this.Configuration);
            services.SetJwtBearer(this.Configuration);
            services.SetAuthorization();
            services.SetMapperDi();
            services.SetCors(this.Configuration);
            services.SetBlobStorage(this.Configuration);
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(this.Configuration["Cors:CorsPolicy"]);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<HubConnect>(this.Configuration["Hub:HubToConnect"]);
            });
        }
    }
}
