using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models.Responses
{
    public class TodayResponse
    {
        [JsonPropertyName("current")]
        public ResponseCurrentBlock Current { get; set; } = new();

       
        [JsonPropertyName("daily")]
        public ResponseDailyBlock Daily { get; set; } = new();
    }
}
