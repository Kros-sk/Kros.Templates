using Kros.AspNetCore.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Kros.CqrsTemplate
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Application entry.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create web host builder.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    string appConfigEndpoint = settings["AppConfig:Endpoint"];
                    if (!string.IsNullOrWhiteSpace(appConfigEndpoint))
                    {
                        config.AddAzureAppConfiguration(hostingContext);
                    }
                    config.AddLocalConfiguration();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
