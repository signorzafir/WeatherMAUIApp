//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Input;
//using WeatherMAUIApp.Models;
//using WeatherMAUIApp.Services;

//namespace WeatherMAUIApp.ViewModels
//{
//    public class ForecastListViewModel : BaseViewModel
//    {
//        private readonly WeatherService _service = new();

//        public ObservableCollection<ForecastDay> Items { get; } = new();

//        private ForecastDay? _selectedItem;
//        public ForecastDay? SelectedItem
//        {
//            get => _selectedItem;
//            set
//            {
//                _selectedItem = value;
//                OnPropertyChanged();
//            }
//        }

//        private LocationQuery? _query;
//        public LocationQuery? Query
//        {
//            get => _query;
//            set { _query = value; OnPropertyChanged(); }
//        }

//        // Simple “query customization”: city picker
//        public List<CityOption> Cities { get; } =
//        [
//            new("Stockholm", 59.3293, 18.0686),
//            new("Göteborg", 57.7089, 11.9746),
//            new("Malmö", 55.6050, 13.0038),
//            new("Umeå", 63.8258, 20.2630),
//            new("Luleå", 65.5848, 22.1547),
//            new("Lahore", 31.5497, 74.3436),

//    ];

//        private CityOption? _selectedCity;
//        public CityOption? SelectedCity
//        {
//            get => _selectedCity;
//            set
//            {
//                _selectedCity = value;
//                OnPropertyChanged();
//                _ = LoadAsync(); // auto-load when city changes
//            }
//        }

//        public ICommand RefreshCommand { get; }

//        public ForecastListViewModel()
//        {
//            RefreshCommand = new Command(async () => await LoadAsync());
//            SelectedCity = Cities[0];
//        }

//        public async Task LoadAsync()
//        {
//            if (IsBusy || SelectedCity is null) return;

//            try
//            {
//                IsBusy = true;
//                ErrorMessage = "";

//                Items.Clear();
//                var data = await _service.GetDailyForecastAsync(SelectedCity.Lat, SelectedCity.Lon);

//                foreach (var day in data)
//                    Items.Add(day);
//            }
//            catch (Exception ex)
//            {
//                ErrorMessage = ex.Message;
//            }
//            finally
//            {
//                IsBusy = false;
//            }
//        }
//    }

//    public record CityOption(string Name, double Lat, double Lon);
//}
using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherMAUIApp.Models;
using WeatherMAUIApp.Services;

namespace WeatherMAUIApp.ViewModels;

public class ForecastListViewModel : BaseViewModel
{
    private readonly WeatherService _service = new();

    public ObservableCollection<ForecastDay> Items { get; } = new();

    // ---------- CITY PICKER ----------

    public ObservableCollection<CityOption> Cities { get; } = new();

    private CityOption? _selectedCity;
    public CityOption? SelectedCity
    {
        get => _selectedCity;
        set
        {
            _selectedCity = value;
            OnPropertyChanged();
            _ = LoadAsync();
        }
    }

    // ---------- NAVIGATION QUERY ----------

    private LocationQuery? _query;
    public LocationQuery? Query
    {
        get => _query;
        set
        {
            _query = value;
            OnPropertyChanged();
            ApplyQuery();
        }
    }

    // ---------- COMMANDS ----------

    public ICommand RefreshCommand { get; }

    // ---------- CONSTRUCTOR ----------

    public ForecastListViewModel(LocationQuery? query = null)
    {
        RefreshCommand = new Command(async () => await LoadAsync());

        BuildCityList();

        Query = query;

        // Fallback if no query passed
        SelectedCity ??= Cities.FirstOrDefault();
    }

    // ---------- METHODS ----------

    private void BuildCityList()
    {
        Cities.Clear();

        Cities.Add(new("Stockholm", 59.3293, 18.0686));
        Cities.Add(new("Göteborg", 57.7089, 11.9746));
        Cities.Add(new("Malmö", 55.6050, 13.0038));
        Cities.Add(new("Umeå", 63.8258, 20.2630));
        Cities.Add(new("Luleå", 65.5848, 22.1547));
        Cities.Add(new("Lahore", 31.5497, 74.3436));
    }

    private void ApplyQuery()
    {
        if (Query is null) return;

        // Insert "Current location" at top
        var current = new CityOption(
            Query.Name,
            Query.Lat,
            Query.Lon,
            IsCurrentLocation: true);

        Cities.Insert(0, current);
        SelectedCity = current;
    }

    public async Task LoadAsync()
    {
        if (IsBusy || SelectedCity is null) return;

        try
        {
            IsBusy = true;
            ErrorMessage = "";

            Items.Clear();
            var data = await _service.GetDailyForecastAsync(
                SelectedCity.Lat,
                SelectedCity.Lon);

            foreach (var day in data)
                Items.Add(day);
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

// ---------- SUPPORT RECORD ----------

//public record CityOption(string Name, double Lat, double Lon);
public record CityOption(string Name, double Lat, double Lon, bool IsCurrentLocation = false)
{
    public string DisplayName =>
        IsCurrentLocation ? $"{Name} (Current location)" : Name;
}


