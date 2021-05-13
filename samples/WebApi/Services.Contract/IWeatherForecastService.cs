using Supercode.Core.ErrorDetails.Attributes;
using System;

namespace WebApi.Services.Contract
{
    public interface IWeatherForecastService
    {
        [ErrorMessage("Could not create weather forecast for date")]
        void Create(DateTime date);
    }
}
