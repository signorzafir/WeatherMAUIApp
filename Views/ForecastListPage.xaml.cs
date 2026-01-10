using WeatherMAUIApp.Models;
using WeatherMAUIApp.ViewModels;

namespace WeatherMAUIApp.Views;

public partial class ForecastListPage : ContentPage
{
    public ForecastListPage(LocationQuery? query)
    {
        InitializeComponent();
        BindingContext = new ForecastListViewModel(query);
        Title = query?.Name ?? "Forecast";
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (BindingContext is not ForecastListViewModel vm) return;

        var item = e.CurrentSelection.FirstOrDefault() as ForecastDay;
        if (item is null) return;

        await Navigation.PushAsync(new ForecastDetailPage(item, vm.SelectedCity!.Name));
        ((CollectionView)sender).SelectedItem = null;
    }
    

}