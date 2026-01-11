using System.Collections.ObjectModel;
using System.Windows.Input;
using WeatherMAUIApp.Models;
using WeatherMAUIApp.Services;

namespace WeatherMAUIApp.ViewModels;

public class ForecastListViewModel : BaseViewModel
{
    private readonly WeatherService _service = new();

    public ObservableCollection<ForecastDay> Items { get; } = new();


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


    public ICommand RefreshCommand { get; }


    public ForecastListViewModel(LocationQuery? query = null)
    {
        RefreshCommand = new Command(async () => await LoadAsync());

        BuildCityList();

        Query = query;

        // Fallback
        SelectedCity ??= Cities.FirstOrDefault();
    }

  

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


public record CityOption(string Name, double Lat, double Lon, bool IsCurrentLocation = false)
{
    public string DisplayName =>
        IsCurrentLocation ? $"{Name} (Current location)" : Name;
}


