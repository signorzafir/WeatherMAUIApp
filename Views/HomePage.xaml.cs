using WeatherMAUIApp.ViewModels;

namespace WeatherMAUIApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is HomeViewModel vm)
            await vm.LoadAsync();
    }
    private async void OnForecastClicked(object sender, EventArgs e)
    {
        if (BindingContext is not HomeViewModel vm)
            return;

        await Navigation.PushAsync(new ForecastListPage(vm.CurrentQuery));
    }
}