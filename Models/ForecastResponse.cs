using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models
{
    public class ForecastResponse
    {
        [JsonPropertyName("daily")]
        public DailyForecast Daily { get; set; } = new();
        [JsonPropertyName("hourly")]
        public HourlyForecast Hourly { get; set; } = new();
    }
}
