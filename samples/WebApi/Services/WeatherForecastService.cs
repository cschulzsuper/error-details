using SharedClasses.Exceptions;
using System;
using WebApi.Services.Contract;

namespace WebApi.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public void Create(DateTime date)
        {
            throw new SharedException($"The weather for the given date '{date}' is unpredictable. No forecast was created.");
        }
    }
}
