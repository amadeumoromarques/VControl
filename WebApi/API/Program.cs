namespace Manufacturer.BaseWebApi.API
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Server;
    using Microsoft.AspNetCore.Hosting.Server.Features;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
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
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                if (args.Contains("migrate")) // Aplica as migrations ao banco de dados
                {
                    CreateHostBuilder(args)
                        .Build()
                        .RunMigrations();
                    return;
                }

                logger.Debug("Iniciando aplicação");

                var host = CreateHostBuilder(args).Build();

                // Executa o host e, em seguida, abre o navegador com o endereço capturado
                host.RunAsync();

                try
                {
                    var serverAddressesFeature = host.Services.GetRequiredService<IServer>()?
                    .Features.Get<IServerAddressesFeature>();

                    if (serverAddressesFeature != null)
                    {
                        var baseUrl = serverAddressesFeature.Addresses.FirstOrDefault();
                        if (!string.IsNullOrEmpty(baseUrl))
                        {
                            var fullUrl = $"{baseUrl}/truck/list";
#if !DEBUG
                        try
                        {
                            OpenBrowser(fullUrl);
                        }
                        catch (Exception)
                        {
                            logger.Info($"Abra o navegador web e digite a URL: {fullUrl}");
                        }
#endif
                        }
                    }
                }
                catch (Exception)
                {
                    logger.Warn($"Verfique no prompt CMD o link para digitar no navegador, ele esta ao lado da frase: 'Now linstening on:........'");
                }

                host.WaitForShutdown();
            }
            catch (Exception exception)
            {
                logger.Error(exception, "A aplicação foi encerrada devido a uma exceção");
                throw;
            }
            finally
            {
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
