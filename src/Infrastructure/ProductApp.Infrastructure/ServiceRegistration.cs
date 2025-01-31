using Microsoft.Extensions.DependencyInjection;
using ProductApp.Infrastructure.Middlewares;

namespace ProductApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
        }
    }
}
