# Super Error Details

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

