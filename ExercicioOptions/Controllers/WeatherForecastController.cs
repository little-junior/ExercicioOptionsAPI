using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ExercicioOptions.Controllers
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
        private readonly IOptionsMonitor<WeatherOptions> _optionsMonitor;
        private readonly IConfiguration _config;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptionsMonitor<WeatherOptions> optionsMonitor, IConfiguration config)
        {
            _logger = logger;
            _optionsMonitor = optionsMonitor;
            _config = config;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            if (_optionsMonitor.CurrentValue.FixedValue)
            {
                //return new List<WeatherForecast>(){
                //    _config.GetSection("FixedWeather").Get<WeatherForecast>()
                //};

                return new WeatherForecast[] { _config.GetSection("FixedWeather").Get<WeatherForecast>()! };
            }

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
            })
            .ToArray();
        }
    }
}
