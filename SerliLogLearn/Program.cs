using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Serilog;
using Serilog.Events;

namespace SerliLogLearn
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                       .MinimumLevel.Debug()
                       .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                       .Enrich.WithMachineName()
                       .Enrich.WithProcessId()
                       .Enrich.WithThreadId()
                       .Enrich.FromLogContext()
                        .WriteTo.TencentCloud(
                            Configuration.GetSection("Serilog:WriteToTencentCloud").GetValue<string>("AppName"),
                            Configuration.GetSection("Serilog:WriteToTencentCloud").GetValue<string>("TopicId"),
                            Configuration.GetSection("Serilog:WriteToTencentCloud").GetValue<string>("RequestBaseUri"))
                        .WriteTo.Console()
                        .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();
    }
}
