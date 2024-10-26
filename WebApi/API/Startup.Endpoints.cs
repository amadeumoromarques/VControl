namespace Manufacturer.BaseWebApi.API
{
    using AI.Infra;
    using Microsoft.AspNet.OData.Extensions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.FileProviders;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Classe de inicialização do sistema inteiro, sub classe.
    /// </summary>
    public partial class Startup
    {
        public void ConfigureServicesEndpoints(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(setup =>
                {
                    setup.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    setup.SerializerSettings.Converters.Add(new StringEnumConverter());
                    setup.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddODataErrorReturn(showDetailedError: ShowDetailedError());

            services.AddRazorPages();
            services.AddOData();
        }

        public void ConfigureUsesEndpoint(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")),
                RequestPath = ""
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                endpoints.MapODataRoute("odata", "odata", objODataBuilder.GetEdmModel());
                endpoints.MapFallbackToFile("index.html"); // Redireciona para index.html
            });
        }
    }
}
