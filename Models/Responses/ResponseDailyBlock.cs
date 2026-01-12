using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models.Responses
{
    public class ResponseDailyBlock
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; } = new();

        [JsonPropertyName("temperature_2m_max")]
        public List<double> Temperature2mMax { get; set; } = new();

        [JsonPropertyName("temperature_2m_min")]
        public List<double> Temperature2mMin { get; set; } = new();

        [JsonPropertyName("weathercode")]
        public List<int> WeatherCode { get; set; } = new();

        [JsonPropertyName("wind_speed_10m_max")]
        public List<double> WindSpeed10mMax { get; set; } = new();

        [JsonPropertyName("precipitation_sum")]
        public List<double> PrecipitationSum { get; set; } = new();

        [JsonPropertyName("sunrise")]
        public List<string> Sunrise { get; set; } = new();

        [JsonPropertyName("sunset")]
        public List<string> Sunset { get; set; } = new();

        [JsonPropertyName("relative_humidity_2m_mean")]
        public List<int> RelativeHumidity2mMean { get; set; } = new();

        [JsonPropertyName("apparent_temperature_mean")]
        public List<double> AvgApparentTemperature { get; set; } = new();

    }
}
