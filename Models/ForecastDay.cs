using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models
{
    public class ForecastDay
    {
        public DateTime Date { get; set; }
        public double MaxTemp { get; set; }
        public double MinTemp { get; set; }
        public int WeatherCode { get; set; }

        public string DateText => Date.ToString("ddd, dd MMM");
        public string TempText => $"{MinTemp:0}° / {MaxTemp:0}°";

        public double WindMax { get; set; }               
        public double PrecipitationSum { get; set; }      
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public int HumidityAvg { get; set; }              


        public string WindText => $"{WindMax:0} km/h";
        public string PrecipText => $"{PrecipitationSum:0.0} mm";
        public string SunriseText => Sunrise.ToString("HH:mm");
        public string SunsetText => Sunset.ToString("HH:mm");
        public string HumidityText => $"{HumidityAvg}%";

        public TimeSpan DayDuration => Sunset - Sunrise;
        public string DayDurationText => $"{(int)DayDuration.TotalHours}h {DayDuration.Minutes}m";


        public string WeatherIcon => WeatherCode switch
        {
            0 =>                        "sun_100.png",                // Clear sky
            1 or 2 =>                   "partly_cloudy_100.png",           // Mainly clear / partly cloudy
            3 =>                        "cloud_100.png",                // Overcast
            45 or 48 =>                 "fog_100.png",         // Fog
            51 or 53 or 55 =>           "rain_cloud_100.png",   // Drizzle
            61 or 63 or 65 =>           "rain_100.png",   // Rain
            66 or 67 =>                 "sleet_100.png",         // Freezing rain
            71 or 73 or 75 or 77 =>     "snow_100.png",   // Snow
            80 or 81 or 82 =>           "rain_100.png",   // Rain showers
            85 or 86 =>                 "snow_100.png",         // Snow showers
            95 =>                       "storm_100.png",               // Thunderstorm
            96 or 99 =>                 "storm_100.png",         // Thunderstorm with hail
            _ =>                        "thermometer_100.png"
        };

        public string WeatherText => WeatherCode switch
        {
            0 => "Clear sky",
            1 or 2 => "Partly cloudy",
            3 => "Cloudy",
            45 or 48 => "Fog",
            51 or 53 or 55 => "Drizzle",
            61 or 63 or 65 => "Rain",
            66 or 67 => "Freezing rain",
            71 or 73 or 75 => "Snow",
            77 => "Snow grains",
            80 or 81 or 82 => "Rain showers",
            85 or 86 => "Snow showers",
            95 => "Thunderstorm",
            96 or 99 => "Thunderstorm with hail",
            _ => "Weather"
        };

    }

}
