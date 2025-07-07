using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;

namespace CloudDemoApp.Controllers
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }
        public int TemperatureC { get; set; }
        public string? Summary { get; set; }
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TelemetryClient _telemetryClient;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, TelemetryClient telemetryClient)
        {
            _logger = logger;
            _telemetryClient = telemetryClient;
        }

        [HttpGet("log-demo")]
        public IEnumerable<WeatherForecast> GetWithLogging()
        {
            _logger.LogInformation("WeatherForecast requested at {Time}", DateTime.UtcNow);
            _logger.LogWarning("WeatherForecast warning example at {Time}", DateTime.UtcNow);
            _telemetryClient.TrackTrace("Custom trace: WeatherForecast endpoint hit");
            _telemetryClient.TrackMetric("WeatherForecastRequests", 1);
            try
            {
                // Simulated error
                if (DateTime.UtcNow.Second % 2 == 0)
                    throw new Exception("Simulated error for logging demo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in WeatherForecast at {Time}", DateTime.UtcNow);
                _telemetryClient.TrackException(ex);
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
} 