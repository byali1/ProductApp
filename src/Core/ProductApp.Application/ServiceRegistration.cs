using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductApp.Application.Validation.FluentValidation;

namespace ProductApp.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assembly);
            services.AddMediatR(assembly);

            //Fluent V.
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<IBaseFluentValidation>();
            services.AddFluentValidationClientsideAdapters();
        }
    }
}
