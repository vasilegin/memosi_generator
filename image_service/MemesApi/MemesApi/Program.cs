using MemesApi.Controllers.Filters;
using MemesApi.Db;
using MemesApi.Starter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Prometheus;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MemesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
            });
            builder.WebHost.UseUrls("http://*:9999");
            
            builder.Host.UseSerilog((context, services, configuration) =>
            {
                configuration
                    .WriteTo.Console(LogEventLevel.Information, outputTemplate: ConfigurationConsts.OutputTemplate)
                    .WriteTo.File("./logs/log.txt", LogEventLevel.Debug, rollingInterval: RollingInterval.Day, outputTemplate: ConfigurationConsts.OutputTemplate)
                    .Enrich.FromLogContext();

                configuration
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);


            });

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<LoggingFilter>();
            });
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<RouteOptions>(conf =>
            {
                conf.LowercaseUrls = true;
            });
            builder.Services.Configure<AppSettings>(config =>
            {
                config.UrlPrefix = builder.Configuration.GetValue<string>(ConfigurationConsts.ApiUrl)
                    + "/static";
                config.MaxImageSize = 10 * 1024 * 1024; // 10 МБ
            });

            builder.Services.AddDbContext<MemeContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetValue<string>(ConfigurationConsts.ConnectionString));
            });

            builder.Services.AddHostedService<MigrationStarter>();
            builder.Services.AddHostedService<ImageIndexer>();
            
            var app = builder.Build();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                     Path.Combine(builder.Environment.ContentRootPath, "static")),
                RequestPath = "/static"
            });
            
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseHttpMetrics();
            app.MapControllers();
            app.MapMetrics();

            app.Run();
        }
    }
}