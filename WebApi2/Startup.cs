using BusinessModels;
using Controller.SimpleController;
using DataServices.DbContexte;
using DataServices.MappingProfiles;
using DataServices.Services;
using DataServices.Services.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi2
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext
            switch (Configuration["DbProvider"])
            {
                case "MsSql":
                    services.AddDbContext<HaushaltsbuchContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("MsSql")); });
                    services.AddDbContext<MsSqlHaushaltsbuchContext>(options => { options.UseSqlServer(Configuration.GetConnectionString("MsSql")); });
                    break;
                case "MySql":
                    services.AddDbContext<HaushaltsbuchContext>(options => { options.UseMySQL(Configuration.GetConnectionString("MySql")); });
                    services.AddDbContext<MySqlHaushaltsbuchContext>(options => { options.UseMySQL(Configuration.GetConnectionString("MySql")); });
                    break;
                default:
                    break;
            }

            // Add Mapping Profiles
            services.AddAutoMapper(
                typeof(BuchungMappingProfile),
                typeof(DauerauftragMappingProfile),
                typeof(GutscheinMappingProfile),
                typeof(KategorieMappingProfile),
                typeof(KonfigurationMappingProfile),
                typeof(RuecklageMappingProfile));

            // Add Controller project
            services.AddMvc()
                .AddApplicationPart(typeof(BuchungController).Assembly)
                .AddApplicationPart(typeof(DauerauftragController).Assembly)
                .AddApplicationPart(typeof(GutscheinController).Assembly)
                .AddApplicationPart(typeof(KategorieController).Assembly)
                .AddApplicationPart(typeof(KonfigurationController).Assembly)
                .AddApplicationPart(typeof(ReportController).Assembly)
                .AddApplicationPart(typeof(RuecklageController).Assembly);

            // Add DataServices
            services.AddTransient<IBuchungDataService, BuchungDataService>();

            // Add CORS rule
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition");
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Auto migrate at startup
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                switch (Configuration["DbProvider"])
                {
                    case "MsSql":
                        var dbContextMsSql = serviceScope.ServiceProvider.GetService<MsSqlHaushaltsbuchContext>();
                        dbContextMsSql.Database.Migrate();
                        break;
                    case "MySql":
                        var dbContextMySql = serviceScope.ServiceProvider.GetService<MySqlHaushaltsbuchContext>();
                        dbContextMySql.Database.Migrate();
                        break;
                    default:
                        break;
                }
            }

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
