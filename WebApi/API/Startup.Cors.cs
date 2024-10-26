namespace Manufacturer.BaseWebApi.API
{
    /// <summary>
    /// Classe de inicialização do sistema inteiro, sub classe.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Configura as services relacionadas a CORS para o sistema funcionar pelo navegador.
        /// </summary>
        /// <param name="services">Services collection.</param>
        public void ConfigureServicesCors(IServiceCollection services)
        {
            services.AddCors();
            services.AddCors(options => options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                .WithOrigins(
                    "http://localhost:9000",
                    "http://localhost:4200",
                    "http://localhost:5000",
                    "http://localhost:5001"
                    )
                .AllowCredentials();
            }));
        }

        /// <summary>
        /// Para configurar o uso do CORS na aplicação.
        /// </summary>
        /// <param name="app">Use the app builder.</param>
        public void ConfigureUsesCors(IApplicationBuilder app)
        {
            app.UseCors("AllowAllOrigins");
        }
    }
}
