namespace Farm;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddAnimalPage),typeof(AddAnimalPage));
        Routing.RegisterRoute(nameof(UpdateAnimalPage),typeof(UpdateAnimalPage));
        Routing.RegisterRoute(nameof(QueryPage),typeof(QueryPage));
        Routing.RegisterRoute(nameof(Forecast), typeof(Farm.Views.Forecast));
    }
}
