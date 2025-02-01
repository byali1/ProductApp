using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ProductApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
           
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() 
                .WriteTo.Console()
                .WriteTo.File("product_app_logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
           
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });
        }
    }
}