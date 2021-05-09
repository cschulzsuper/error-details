using System;
using System.Collections.Generic;
using Super.Core.ErrorDetails.Attributes;

namespace WebApi.Services.Contract
{
    public interface IWeatherForecastService
    {
        [ErrorMessage("Could not create weather forecast for date")]
        void Create(DateTime date);
    }
}
