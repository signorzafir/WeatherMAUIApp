using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models.Responses
{
    public class ForecastResponse
    {
        [JsonPropertyName("daily")]
        public ResponseDailyBlock Daily { get; set; } = new();
        [JsonPropertyName("hourly")]
        public ResponseHourlyBlock Hourly { get; set; } = new();
    }
}
