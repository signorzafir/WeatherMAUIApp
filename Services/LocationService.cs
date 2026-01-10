using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherMAUIApp.Models;
using WeatherMAUIApp.ViewModels;
using static System.Net.WebRequestMethods;


namespace WeatherMAUIApp.Services
{
    public class LocationService
    {
        private readonly HttpClient _http = new();

        public async Task<(Location? location, string? city)> GetCurrentLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(
                    GeolocationAccuracy.Medium,
                    TimeSpan.FromSeconds(10));

                var location = await Geolocation.Default.GetLocationAsync(request);
                if (location == null) return (null, null);

                string? city = null;
                try
                {
                    city = await GetCityNameAsync(location.Latitude, location.Longitude);
                }
                catch
                {
                    city = null;
                }

                return (location, city);
            }
            catch
            {
                return (null, null);
            }
        }

        public async Task<string?> GetCityNameAsync(double lat, double lon, string language = "sv")
        {
            using var response = await _http.GetAsync(
                BuildUrl(lat, lon, language));

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<ReverseGeocodeResponse>();

            return
                !string.IsNullOrWhiteSpace(data?.City) ? data!.City :
                !string.IsNullOrWhiteSpace(data?.Locality) ? data!.Locality :
                !string.IsNullOrWhiteSpace(data?.Region) ? data!.Region :
                null;
        }

        private static string BuildUrl(double lat, double lon, string language)
        {
            var latStr = lat.ToString(CultureInfo.InvariantCulture);
            var lonStr = lon.ToString(CultureInfo.InvariantCulture);

            return
                $"https://api.bigdatacloud.net/data/reverse-geocode-client" +
                $"?latitude={latStr}&longitude={lonStr}&localityLanguage={language}";
        }
    }


}

