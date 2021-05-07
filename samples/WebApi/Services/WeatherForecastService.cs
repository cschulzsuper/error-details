using System;
using System.Collections.Generic;
using SharedClasses.Exceptions;
using WebApi.Services.Contract;

namespace WebApi.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Create(DateTime date)
        {
            throw new SharedException($"The weather for the given date '{date}' is unpredictable. No forecast was created.");
        }
    }
}
