using WeatherMAUIApp.Models;
using WeatherMAUIApp.ViewModels;

namespace WeatherMAUIApp.Views;

public partial class ForecastDetailPage : ContentPage
{
	public ForecastDetailPage(ForecastDay day, string cityName)
	{
		InitializeComponent();
        BindingContext = new ForecastDetailViewModel(day, cityName);
    }
}