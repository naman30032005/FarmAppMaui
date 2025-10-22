namespace Farm.ViewModels;

// Implementation part for the partial property
public partial class DashboardVM : ObservableObject
{
    private readonly DbOps _dbOps;

    [ObservableProperty]
    private int totalAnimal;

    public DashboardVM()
    {
        _dbOps = new DbOps();
        LoadTotalsAnimal();
    }

    [RelayCommand]
    public async Task LoadTotalsAnimal()
    {
        totalAnimal = await _dbOps.GetTotalAnimalsAsync();
    }

    [RelayCommand]
    public async Task ForecastPage()
    {
        await Shell.Current.GoToAsync($"{nameof(Forecast)}", true);
    }
}

