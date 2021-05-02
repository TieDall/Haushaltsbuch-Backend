using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices((hostContext, services) =>
            {
                // Set the active provider via configuration
                var configuration = hostContext.Configuration;
                var provider = configuration.GetValue("Provider", "SqlServer");

                services.AddDbContext<HaushaltsbuchContext>(
                    options => _ = provider switch
                    {
                        "MySql" => options.UseMySQL(
                            "server=ip-adress;database=Haushaltsbuch;user=username;password=password"),

                        "SqlServer" => options.UseSqlServer(
                            @"Server=(localdb)\mssqllocaldb;Database=Haushaltsbuch;Integrated Security=True"),

                        _ => throw new Exception($"Unsupported provider: {provider}")
                    });
            });
    }
}
