# Error Details

Extensible aspect oriented error detail handler to analyze exceptions.

[![Build](https://img.shields.io/github/workflow/status/cschulzsuper/error-details/Deploy%20Master)](https://github.com/cschulzsuper/error-details/actions?query=workflow%3A"Deploy+Master")
[![Nuget](https://img.shields.io/github/v/release/cschulzsuper/error-details?sort=semver)](https://github.com/cschulzsuper/error-details/packages/)

## Getting Started
Once the first release is ready, it will be available on [Nuget](https://www.nuget.org/).  
You can download a preview version [here](https://github.com/cschulzsuper/error-details/packages/).

## Overview

This library makes it possible to give an exception some additional meaning.
This magic happens, by providing aspects/annotation for methods or types, which are part of the call stack.

Once an exception needs to be handled (e.g. by an eror handling in the ASP.NET Core Middelware Pipline), the `IErrorDetailsGenerator` can be used,
to analyze the exception and its call stack.

:warning: **Parts of the code are experimental. First and foremost the detection of asynchronous methods.**

## Usage

### Add Service

The following service registration is a basic example.

```csharp

services.AddErrorDetails(options =>
{
    // Enable stack frame processing for additional messages
    options.EnableStackTraceIteration(); 

    // This will add the target site (Exception.TargetSite or StackFrame.GetMethod())
    // to the members that need to be processed
    options.Filters.TargetSite(); 
    
    // This will handle async target site methods, which behave differently on the stack trace
    options.Filters.TargetSiteAsync(); 
    
    // This will get a message from an attribute annotation on the target site 
    // (called for the exception and stack frame processing) 
    options.Filters.ErrorMessageFromAnnotation<ErrorMessageAttribute>(x => x.Message);
    
    // This will process the exception message (Skipped in case of a stack frame)
    options.Filters.ErrorMessageFromException();
})
```

### Annotate Methods

The service was configured with error messages from annotations, which means that the rest of the program  can now use the `ErrorMessageAttribute` to specify meaningful error messages. Error Details will gather those messages and combine them into an error object, if an exception is thrown deep down in the call stack.

```csharp
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    /* removed for brevity */

    [HttpGet]
    [ErrorMessage("Could not process get for weather forecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecastService.Create(DateTime.Now);
    }
}
`````

```csharp
    public class WeatherForecastService
    {
        [ErrorMessage("Could not get weather forecast for date")]
        public IEnumerable<WeatherForecast> Create(DateTime date)
        {
            throw new NotImplementedException("Weather forecast is not yet implemented");
        }
    }
`````

### Handle Exception

Exceptions can be handled in multiple scenarios. 
The following code shows an example in ASP.NET Core.

```csharp
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseExceptionHandler(appBuilder => appBuilder.Run(HandleError));

    /* removed for brevity */
}

public async Task HandleError(HttpContext context)
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature.Error;

    var errorDetailGenerator = context.RequestServices.GetRequiredService<IErrorDetailsGenerator>();
    var errorResult = await errorDetailGenerator
        .GenerateAsync(exception);

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

    await context.Response.WriteAsJsonAsync(errorResult);
}
```

### Process Error

The above example could be processed and displayed on the client-side.

> ---
> An error has occurred
>  - Could not process get for weather forecast
>  - Could not get weather forecast for date
>  - Weather forecast is not yet implemented
> ---

This gives the user of your product and the first  level support of your company a rough picture about the error. If your error messages are written consequently and meaningful, an elevation to the second or even the third level support can be prevented.
