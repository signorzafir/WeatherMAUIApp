using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models
{
    public class ReverseGeocodeResponse
    {
        [JsonPropertyName("city")] public string? City { get; set; }
        [JsonPropertyName("locality")] public string? Locality { get; set; }
        [JsonPropertyName("principalSubdivision")] public string? Region { get; set; }
        [JsonPropertyName("countryName")] public string? Country { get; set; }
    }
}
