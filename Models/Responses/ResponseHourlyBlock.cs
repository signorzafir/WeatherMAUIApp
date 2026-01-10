using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models.Responses
{
    public class ResponseHourlyBlock
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = new();

        [JsonPropertyName("relative_humidity_2m")]
        public List<int> RelativeHumidity2m { get; set; } = new();
    }
}
