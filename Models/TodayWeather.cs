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
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public double PrecipitationSum { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public int CurrentWeatherCode { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public string CurrentTempText => $"{CurrentTemp:0}°C";
        public string CurrentFeelsLikeText => $"{FeelsLike:0}°C";
        public string CurrentPrecipText => $"{PrecipitationSum:0.0} mm";
        public string TodayMaxMinText => $"Max {MaxTemp:0}° / Min {MinTemp:0}°";
        public string TodaySunriseText => Sunrise.ToString("HH:mm");
        public string TodaySunsetText => Sunset.ToString("HH:mm");

        public TimeSpan DayDuration => Sunset - Sunrise;
        public string TodayDayDurationText => $"{(int)DayDuration.TotalHours}h {DayDuration.Minutes}m";
    }
}
