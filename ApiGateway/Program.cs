using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                   .UseUrls("http://*:2000")
                   .ConfigureAppConfiguration((hostingContext, config) =>
                   {
                       config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                           .AddJsonFile("OcelotConfig.json")
                           .AddEnvironmentVariables();
                   })
               .ConfigureServices(s =>
               {
                   s.AddOcelot();
                   s.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
               })
               .Configure(o =>
                {
                    o.UseOcelot().Wait();
               });
    }
}
