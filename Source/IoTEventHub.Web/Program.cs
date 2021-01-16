using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IoTEventHub.Web
{
    /// <summary>
    /// The entry point for this project
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main method
        /// </summary>
        /// <param name="args">The arguments</param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// The host builder
        /// Triggers execution of Startup.ConfigureContainer(), see https://github.com/autofac/Examples/blob/master/src/AspNetCore3Example/Program.cs
        /// </summary>
        /// <param name="args">The arguments</param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
