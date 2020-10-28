using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace api1.Controllers
{
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
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("self")]
        public async Task<IEnumerable<WeatherForecast>> SelfAsync()
        {
             using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync("http://api1/weatherforecast");
                var content = await result.Content.ReadAsStreamAsync();
                var items = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(content);
                return items;
            }
        }
        [HttpGet("other")]
        public async Task<IEnumerable<WeatherForecast>> OtherAsync()
        {
             using (HttpClient client = new HttpClient())
            {
                var result = await client.GetAsync("http://api2/weatherforecast");
                var content = await result.Content.ReadAsStreamAsync();
                var items = await JsonSerializer.DeserializeAsync<List<WeatherForecast>>(content);
                return items;
            }
        }
    }
}
