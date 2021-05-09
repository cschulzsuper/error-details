using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Super.Core.ErrorDetails.Attributes;
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
        [ErrorType("api-error")]
        [ErrorMessage("Could not process post for weather forecast")]
        public IActionResult Post()
        {
            _service.Create(DateTime.Now);
            return Ok(DateTime.Now);
        }
    }
}
