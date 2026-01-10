namespace WeatherMAUIApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
        }
        //protected override Window CreateWindow(IActivationState? activationState)
        //{
        //    return new Window(new NavigationPage(new Views.ForecastListPage()));
        //}
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new Views.HomePage()));
        }

    }
}
