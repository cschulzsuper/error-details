using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SharedClasses.Exceptions;
using Supercode.Core.ErrorDetails.Attributes;
using Supercode.Core.ErrorDetails.DependencyInjection;
using Supercode.Core.ErrorDetails.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorWebAssembly
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddErrorDetails(options =>
            {
                options.EnableStackTraceIteration();

                options.Filters.TargetSite();
                options.Filters.TargetSiteAsync();
                options.Filters.ErrorCode("about:blank");
                options.Filters.ErrorMessageFromAnnotation<ErrorMessageAttribute>(x => x.Message);
                options.Filters.ErrorMessageFromException<SharedException>(x => x.Message);
            });

            await builder.Build().RunAsync();
        }
    }
}
