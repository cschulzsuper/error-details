using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Super.Core.ErrorDetails.Attributes;
using Super.Core.ErrorDetails.Options;
using Super.Core.ErrorDetails.Providers;

namespace Super.Core.ErrorDetails.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddErrorDetailsDefault(this IServiceCollection services)
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
                    options.Filters.ErrorType("about:blank");
                    options.Filters.ErrorTypeFromAnnotation<ErrorTypeAttribute>(x => x.Type);
                    options.Filters.ErrorTypeFromService<IErrorTypeProvider<MemberInfo>>();
                    options.Filters.ErrorMessage("unknown error");
                    options.Filters.ErrorMessageFromException();
                    options.Filters.ErrorMessageFromAnnotation<ErrorMessageAttribute>(x => x.Message);
                    options.Filters.ErrorMessageFromService<IErrorMessageProvider<MemberInfo>>();
                });
        }
    }
}
