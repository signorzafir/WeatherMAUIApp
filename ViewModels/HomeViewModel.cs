using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherMAUIApp.Models;
using WeatherMAUIApp.Services;

namespace WeatherMAUIApp.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private readonly LocationService _locationService = new();
        private readonly WeatherService _weatherService = new();

        private TodayWeather? _today;

        public TodayWeather Today
        {
            get { return _today; }
            set
            {
                _today = value;
                OnPropertyChanged();
            }
        }
        private LocationQuery? _currentQuery;
        public LocationQuery? CurrentQuery
        {
            get => _currentQuery;
            set { _currentQuery = value; OnPropertyChanged(); }
        }

        public ICommand LoadCommand { get; }

        public HomeViewModel()
        {
            LoadCommand = new Command(async () => await LoadAsync());
        }

        public async Task LoadAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                ErrorMessage = string.Empty;
                var (location, place) = await _locationService.GetCurrentLocationAsync();

                



                //Setting Stockholm as default location in case location is null

                double lat = location?.Latitude ?? 59.3293; // Default to Stockholm
                double lon = location?.Longitude ?? 18.0686;// Default to Stockholm

                var today = await _weatherService.GetTodayAsync(lat, lon);

                CurrentQuery = new LocationQuery
                {
                    Name = !string.IsNullOrWhiteSpace(place) ? place! : "Current location",
                    Lat = lat,
                    Lon = lon
                };

                var cityLabel = !string.IsNullOrWhiteSpace(place)
                    ? $"{place} (Current Location)"
                    : "Stockholm (Default)";

                today.CityName = location is null ? "Stockholm (Default)" :
                    !string.IsNullOrWhiteSpace(place) ? $"{place} (Current Location)" :
                    "Current location";

                Today = today;
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
