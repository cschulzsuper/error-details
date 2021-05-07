using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using SharedClasses.Exceptions;
using SharedClasses.JsonConverters;
using Super.Core.ErrorDetails;
using Super.Core.ErrorDetails.Attributes;
using Super.Core.ErrorDetails.Extensions;
using Super.Core.ErrorDetails.Options;
using WebApi.Services;
using WebApi.Services.Contract;

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
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApi", Version = "v1"});
            });

            services.AddScoped<IWeatherForecastService, WeatherForecastService>();

            services.AddErrorDetails(options =>
            {
                options.EnableStackTraceIteration();

                options.Filters.TargetSite();
                options.Filters.TargetSiteContract();
                options.Filters.ErrorType("about:blank");
                options.Filters.ErrorTypeFromAnnotation<ErrorTypeAttribute>(attribute => $"https://example.com/probs/{attribute.Type}");
                options.Filters.ErrorMessageFromAnnotation<ErrorMessageAttribute>(attribute => attribute.Message);
                options.Filters.ErrorMessageFromException<SharedException>(exception => exception.Message);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(appBuilder => appBuilder.Run(HandleError));

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public async Task HandleError(HttpContext context)
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature.Error;

            var errorDetailGenerator = context.RequestServices.GetRequiredService<IErrorDetailsGenerator>();
            var errorResult = await errorDetailGenerator
                .GenerateAsync(exception);

            var httpMethod = context.Request.Method;
            var statusCode = httpMethod switch
            {
                _ when HttpMethods.IsGet(httpMethod) => StatusCodes.Status404NotFound,
                _ when HttpMethods.IsHead(httpMethod) => StatusCodes.Status404NotFound,
                _ when HttpMethods.IsPost(httpMethod) => StatusCodes.Status400BadRequest,
                _ when HttpMethods.IsPut(httpMethod) => StatusCodes.Status400BadRequest,
                _ when HttpMethods.IsPatch(httpMethod) => StatusCodes.Status400BadRequest,
                _ when HttpMethods.IsDelete(httpMethod) => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            context.Response.StatusCode = statusCode;


            var jsonSerializerOptions = new JsonSerializerOptions();

            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.IgnoreNullValues = true;
            jsonSerializerOptions.Converters.Add(new FormattableStringConverter());

            await context.Response.WriteAsJsonAsync(errorResult, jsonSerializerOptions);
        }
    }
}
