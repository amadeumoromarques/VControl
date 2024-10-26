namespace Manufacturer.BaseWebApi.API
{
    using Manufacturer.Project.Core.DataContext;
    using Marques.AI.Infra;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Classe de inicialização do sistema inteiro.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">the configuration.</param>
        /// <param name="environment">the environment.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Environment variable.
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Gets the configuration variable.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">services.</param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ModuleConfigExtensions.AddAutoRegister<ProjectDBContext>(services);

            ConfigureEntityFramework(services);

            services.AddHttpClient();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            ConfigureServicesOData();
            ConfigureServicesHealthChecks(services);
            ConfigureServicesCors(services);
            ConfigureServicesEndpoints(services);

            ConfigureHostedServices(services);

            services.AddSignalR()
                .AddJsonProtocol(e => e.PayloadSerializerOptions.PropertyNamingPolicy = null);
        }

        /// <summary>
        /// Configure.
        /// </summary>
        /// <param name="app">App.</param>
        public virtual void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            // Configura serviços e verificações iniciais
            ConfigureUsesServicesChecks(app);

            // Verifica se o ambiente não é de desenvolvimento, então aplica o HSTS
            if (!Environment.IsDevelopment())
            {
                // O tempo default do HSTS é de 30 dias, se você deseja alterar o tempo, verifique em aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            // Inicializa o banco de dados local usando Migrations
            // Aplica as migrations automaticamente
            using (var scope = serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<ProjectDBContext>();

                // Aplica as migrations
                context.Database.Migrate();
            }

            // Configura CORS e endpoints
            ConfigureUsesCors(app);
            ConfigureUsesEndpoint(app);
        }

        /// <summary>
        /// Configura o entity para acessar o banco de dados utilizando a connection string definida anteriormente.
        /// </summary>
        /// <param name="services">params.</param>
        protected void ConfigureEntityFramework(IServiceCollection services)
        {
            services.AddDbContext<ProjectDBContext>(options =>
                options.UseSqlite(
                    "Data Source=collector.db", b => b.MigrationsAssembly("BaseWebApi")
                ));
        }

        /// <summary>
        /// Show errors.
        /// </summary>
        /// <returns>error code.</returns>
        protected virtual bool ShowDetailedError()
        {
            return Environment.IsDevelopment();
        }
    }
}