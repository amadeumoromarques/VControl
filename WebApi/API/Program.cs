namespace Manufacturer.BaseWebApi.API
{
    using System;
    using System.Diagnostics;
    using Manufacturer.Project.Core.DataContext;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using NLog.Web;

    /// <summary>
    /// The program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Program"/> class.
        /// </summary>
        protected Program()
        {
        }

        /// <summary>
        /// The main.
        /// </summary>
        public static void Main(string[] args)
        {
            /*
             * Log error levels
             * https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0#log-level
             */

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                if (args.Contains("migrate")) // Aplica as migrations ao banco de dados
                {
                    CreateHostBuilder(args)
                        .Build()
                        .RunMigrations();
                    return;
                }

                logger.Debug("init main");

#if !DEBUG
    OpenBrowser("http://localhost:5001/truck/list");
#endif

                CreateHostBuilder(args)
                    .Build()
                    .Run();  // Inicia a aplicação como servidor Web
            }
            catch (Exception exception)
            {
                ////NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        private static void OpenBrowser(string url)
        {
            try
            {
                // Abre o navegador com a URL específica
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao tentar abrir o navegador, abra o navegador usando a url: {url}");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    var nlogEnabled = hostingContext.Configuration.GetValue<bool>("Logging:NLog:Enabled");

                    if (nlogEnabled)
                    {
                        logging.ClearProviders();
                        logging.AddNLog("nlog.config");
                    }
                });
        }
    }
}