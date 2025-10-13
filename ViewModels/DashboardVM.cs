
namespace Farm.ViewModels;

public partial class DashboardVM
{
    [RelayCommand]
    public async Task ForecastPage()
    {
        await Shell.Current.GoToAsync($"{nameof(Forecast)}", true);
    }
}
