using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models
{
    public class TodayResponse
    {
        [JsonPropertyName("current")]
        public CurrentBlock Current { get; set; } = new();

        //[JsonPropertyName("hourly")]
        //public HourlyBlock Hourly { get; set; } = new();

        [JsonPropertyName("daily")]
        public DailyBlock Daily { get; set; } = new();

        public class CurrentBlock
        {
            [JsonPropertyName("temperature_2m")]
            public double Temperature2m { get; set; }
        }

        public class HourlyBlock
        {
            [JsonPropertyName("time")]
            public List<string> Time { get; set; } = new();

            [JsonPropertyName("relative_humidity_2m")]
            public List<int> RelativeHumidity2m { get; set; } = new();

            [JsonPropertyName("apparent_temperature")]
            public List<double> ApparentTemperature { get; set; } = new();
        }

        public class DailyBlock
        {
            [JsonPropertyName("temperature_2m_max")]
            public List<double> Temperature2mMax { get; set; } = new();

            [JsonPropertyName("temperature_2m_min")]
            public List<double> Temperature2mMin { get; set; } = new();

            [JsonPropertyName("sunrise")]
            public List<string> Sunrise { get; set; } = new();

            [JsonPropertyName("sunset")]
            public List<string> Sunset { get; set; } = new();
        }
    }
}
