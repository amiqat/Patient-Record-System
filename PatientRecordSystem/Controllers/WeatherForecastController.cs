using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PatientRecordSystem.Extensions;
using PatientRecordSystem.Helpers;
using PatientRecordSystem.Resources;

namespace PatientRecordSystem.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAsync([FromQuery] QueryStringParameters queryParameters)
        {
            var rng = new Random();
            var columnsMap = new Dictionary<string, Expression<Func<WeatherForecast, object>>>()
            {
                ["date"] = v => v.Date,
                ["temperatureC"] = v => v.TemperatureC,
                ["summary"] = v => v.Summary
            };
            var list = PagedList<WeatherForecast>.ToPagedList(Enumerable.Range(1, 20).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).AsQueryable().Sort(queryParameters.SortBy, columnsMap, queryParameters.IsSortAscending), queryParameters.PageNumber, queryParameters.PageSize);
            var metadata = new
            {
                list.TotalCount,
                list.PageSize,
                list.CurrentPage,
                list.TotalPages,
                list.HasNext,
                list.HasPrevious
            };
            Response.Headers["X-Pagination"] = JsonConvert.SerializeObject(metadata);
            return Ok(list);
        }
    }
}