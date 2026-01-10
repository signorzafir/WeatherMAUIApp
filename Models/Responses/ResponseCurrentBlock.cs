using System.Text.Json.Serialization;

namespace WeatherMAUIApp.Models.Responses
{
    
        public class ResponseCurrentBlock
        {
            [JsonPropertyName("temperature_2m")]
            public double Temperature2m { get; set; }

            [JsonPropertyName("relative_humidity_2m")]
            public int RelativeHumidity2m { get; set; }

            [JsonPropertyName("apparent_temperature")]
            public double ApparentTemperature { get; set; }

            [JsonPropertyName("precipitation")]
            public double Precipitation { get; set; }

            [JsonPropertyName("weather_code")]
            public int WeatherCode { get; set; }

            [JsonPropertyName("wind_speed_10m")]
            public double WindSpeed10m { get; set; }

        }
    }

