using Serilog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Jobby
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog((context, config) => config
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .WriteTo.File(path: context.Configuration["Serilog:FilePath"]))
                .UseUrls("https://localhost:8000");
    }
}
