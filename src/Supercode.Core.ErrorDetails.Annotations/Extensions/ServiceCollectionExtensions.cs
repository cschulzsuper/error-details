using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Supercode.Core.ErrorDetails.Attributes;
using Supercode.Core.ErrorDetails.Options;
using Supercode.Core.ErrorDetails.Providers;

namespace Supercode.Core.ErrorDetails.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddErrorDetailsWithAnnotations(this IServiceCollection services)
        {
            return services
                .AddErrorDetails(options =>
                {
                    options.EnableStackTraceIteration();
                    options.EnableInnerErrorsFromInnerExceptions();

                    options.Filters.TargetSite();
                    options.Filters.TargetSiteAsync();
                    options.Filters.TargetSiteContract();
                    options.Filters.TargetSiteDeclaringType();
                    options.Filters.InnerException();
                    options.Filters.InnerExceptions();
                    options.Filters.ErrorCodeFromAnnotation<ErrorCodeAttribute>(x => x.Type);
                    options.Filters.ErrorMessageFromException();
                    options.Filters.ErrorMessageFromAnnotation<ErrorMessageAttribute>(x => x.Message);
                });
        }
    }
}
