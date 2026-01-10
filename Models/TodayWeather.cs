 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models
{
    public class TodayWeather
    {
        public string CityName { get; set; } = "Current location";
        public double CurrentTemp { get; set; }
        public double CurrentFeelsLike { get; set; }
        public int CurrentHumidity { get; set; }
        public double PrecipitationSum { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public int CurrentWeatherCode { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public string CurrentTempText => $"{CurrentTemp:0}°C";
        public string CurrentFeelsLikeText => $"{CurrentFeelsLike:0}°C";
        public string CurrentPrecipText => $"{PrecipitationSum:0.0} mm";
        public string TodayMaxMinText => $"Max {MaxTemp:0}° / Min {MinTemp:0}°";
        public string TodaySunriseText => Sunrise.ToString("HH:mm");
        public string TodaySunsetText => Sunset.ToString("HH:mm");

        public TimeSpan DayDuration => Sunset - Sunrise;
        public string TodayDayDurationText => $"{(int)DayDuration.TotalHours}h {DayDuration.Minutes}m";

        public string CurrentWeatherIcon => CurrentWeatherCode switch
        {
            0 => "☀️",
            1 or 2 => "🌤️",
            3 => "☁️",
            45 or 48 => "🌫️",
            51 or 53 or 55 => "🌦️",
            61 or 63 or 65 => "🌧️",
            71 or 73 or 75 => "❄️",
            80 or 81 or 82 => "🌧️",
            95 or 96 or 99 => "⛈️",
            _ => "🌡️"
        };

        public string CurretnWeatherText => CurrentWeatherCode switch
        {
            0 => "Clear sky",
            1 or 2 => "Partly cloudy",
            3 => "Cloudy",
            45 or 48 => "Fog",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rain",
            71 or 73 or 75 => "Snow",
            80 or 81 or 82 => "Rain showers",
            95 or 96 or 99 => "Thunderstorm",
            _ => "Weather"
        };
    }
}
