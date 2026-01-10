using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMAUIApp.Models
{
    public class LocationQuery
    {
        public string Name { get; set; } = "Current location";
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}
