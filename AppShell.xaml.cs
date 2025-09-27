namespace Farm;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(AddAnimalPage),typeof(AddAnimalPage));
    }
}
