namespace Manufacturer.BaseWebApi.API
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;

    /// <summary>
    /// Classe de inicialização do sistema inteiro, sub classe.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configura o serviço do sistema.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public void ConfigureServicesHealthChecks(IServiceCollection services)
        {
            services.AddHealthChecks();

            var section = Configuration.GetSection("Environments");
            var children = section.GetChildren();

            foreach (var child in children)
            {
                var name = child.Key;
                var connString = child.GetValue<string>("ConnectionString");

                services
                    .AddHealthChecks()
                    .AddSqlServer(connString, name: name);
            }
        }

        /// <summary>
        /// Configura o endpoint para o healthz da aplicação.
        /// </summary>
        /// <param name="app">The app builder.</param>
        public void ConfigureUsesServicesChecks(IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthz", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
            });
        }
    }
}
