using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherMAUIApp.Models;

namespace WeatherMAUIApp.ViewModels
{
    public class ForecastDetailViewModel
    {
        public string CityName { get; }
        public ForecastDay Day { get; set; }

        public ForecastDetailViewModel(ForecastDay day, string cityName)
        {
            Day = day;
            CityName = cityName;
        }
        public string DateText => Day.DateText;
        public string TempText => Day.TempText;
        public int WeatherCode => Day.WeatherCode;

        public string BigTempText => $"{Day.MaxTemp:0}°";
        public string MinMaxText => $"Min {Day.MinTemp:0}°  •  Max {Day.MaxTemp:0}°";
        public string WindText => Day.WindText;
        public string PrecipText => Day.PrecipText;
        public string HumidityText => Day.HumidityText;
        public string SunriseText => Day.SunriseText;
        public string SunsetText => Day.SunsetText;
        public string DayDurationText => Day.DayDurationText;


    }
}
