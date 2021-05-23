using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });

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

            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            // migrate at Startup
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
