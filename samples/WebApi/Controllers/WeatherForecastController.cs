using Microsoft.AspNetCore.Mvc;
using Supercode.Core.ErrorDetails.Attributes;
using System;
using WebApi.Services.Contract;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpPost]
        [ErrorCode("api-error")]
        [ErrorMessage("Could not process post for weather forecast")]
        public IActionResult Post()
        {
            _service.Create(DateTime.Now);
            return Ok(DateTime.Now);
        }
    }
}
