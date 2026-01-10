using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherMAUIApp.Models;
using WeatherMAUIApp.Models.Responses;

namespace WeatherMAUIApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _http = new();

        public async Task<TodayWeather> GetTodayAsync(double lat, double lon)
        {
            var latStr = lat.ToString(CultureInfo.InvariantCulture);
            var lonStr = lon.ToString(CultureInfo.InvariantCulture);

            var url =
            $"https://api.open-meteo.com/v1/forecast" +
            $"?latitude={latStr}&longitude={lonStr}" +
            $"&current=temperature_2m,relative_humidity_2m,apparent_temperature,precipitation,weather_code,wind_speed_10m" +
            $"&daily=temperature_2m_max,temperature_2m_min,sunrise,sunset,precipitation_sum,wind_speed_10m_max" +
            $"&timezone=auto";

            var response = await _http.GetFromJsonAsync<TodayResponse>(url)
                           ?? throw new Exception("Could not retreive weather data...");

            //int timeIndex = FindClosestHourIndex(response.Hourly.Time);

            var today = new TodayWeather
            {
                CurrentTemp = response.Current.Temperature2m,
                CurrentFeelsLike = response.Current.ApparentTemperature,
                CurrentWeatherCode = response.Current.WeatherCode,
                CurrentHumidity = response.Current.RelativeHumidity2m,
                MaxTemp = response.Daily.Temperature2mMax.FirstOrDefault(),
                MinTemp = response.Daily.Temperature2mMin.FirstOrDefault(),
                Sunrise = DateTime.Parse(response.Daily.Sunrise.First()),
                Sunset = DateTime.Parse(response.Daily.Sunset.First())
            };
            return today;
        }

        //private static int FindClosestHourIndex(List<string> time)
        //{
        //    if (time.Count == 0)
        //        return 0;
        //    var timeNow = DateTime.Now;
        //    int closestIndex = 0;
        //    TimeSpan closestDiff = TimeSpan.MaxValue;

        //    for (int i = 0; i < time.Count; i++)
        //    {
        //        if (DateTime.TryParse(time[i], out var t))
        //        {
        //            var diff = (t - timeNow).Duration();

        //            if (diff < closestDiff)
        //            {
        //                closestDiff = diff;
        //                closestIndex = i;
        //            }
        //        }
        //    }
        //    return closestIndex;
        //}

        
        public async Task<List<ForecastDay>> GetDailyForecastAsync(double lat, double lon)
        {
            var latStr = lat.ToString(CultureInfo.InvariantCulture);
            var lonStr = lon.ToString(CultureInfo.InvariantCulture);

            var url =
                $"https://api.open-meteo.com/v1/forecast" +
                $"?latitude={latStr}&longitude={lonStr}" +
                $"&daily=temperature_2m_max,temperature_2m_min,weathercode,wind_speed_10m_max,precipitation_sum,sunrise,sunset" +
                $"&hourly=relative_humidity_2m" +
                $"&timezone=auto";

            var response = await _http.GetFromJsonAsync<ForecastResponse>(url)
                          ?? throw new Exception("No data returned from weather service.");

            var d = response.Daily;
            var h = response.Hourly;

            int count = new[]
            {
            d.Time.Count,
            d.Temperature2mMax.Count,
            d.Temperature2mMin.Count,
            d.WeatherCode.Count,
            d.WindSpeed10mMax.Count,
            d.PrecipitationSum.Count,
            d.Sunrise.Count,
            d.Sunset.Count
        }.Min();

            var result = new List<ForecastDay>();

            for (int i = 0; i < count; i++)
            {
                var dayDate = DateTime.Parse(d.Time[i]).Date;

                // Compute average humidity for that day from hourly arrays
                int humidityAvg = CalculateDailyAverageHumidity(dayDate, h);

                result.Add(new ForecastDay
                {
                    Date = dayDate,
                    MaxTemp = d.Temperature2mMax[i],
                    MinTemp = d.Temperature2mMin[i],
                    WeatherCode = d.WeatherCode[i],

                    WindMax = d.WindSpeed10mMax[i],
                    PrecipitationSum = d.PrecipitationSum[i],
                    Sunrise = DateTime.Parse(d.Sunrise[i]),
                    Sunset = DateTime.Parse(d.Sunset[i]),
                    HumidityAvg = humidityAvg
                });
            }

            return result;
        }

        private int CalculateDailyAverageHumidity(DateTime day, ResponseHourlyBlock h)
        {
            if (h.Time.Count == 0 || h.RelativeHumidity2m.Count == 0)
                return 0;

            int count = Math.Min(h.Time.Count, h.RelativeHumidity2m.Count);

            int sum = 0;
            int n = 0;

            for (int i = 0; i < count; i++)
            {
                if (!DateTime.TryParse(h.Time[i], out var t))
                    continue;

                if (t.Date != day)
                    continue;

                sum += h.RelativeHumidity2m[i];
                n++;
            }

            return n > 0 ? (int)Math.Round(sum / (double)n) : 0;
        }
    }
    
}
