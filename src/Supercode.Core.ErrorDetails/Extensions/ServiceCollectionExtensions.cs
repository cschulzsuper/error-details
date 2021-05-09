using System;
using Microsoft.Extensions.DependencyInjection;
using Supercode.Core.ErrorDetails.Options;

namespace Supercode.Core.ErrorDetails.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddErrorDetails(this IServiceCollection services, Action<ErrorDetailsOptions> configure)
        {
            services.AddScoped<IErrorDetailsGenerator, ErrorDetailsGenerator>();
            services.AddScoped<IErrorDetailFilterProvider, ErrorDetailFilterProvider>();
            
            services.Configure(configure);

            return services;
        }
    }
}
