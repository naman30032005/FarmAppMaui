namespace Farm.ViewModels;

public partial class QueryPageVM: BaseVM
{
    [ObservableProperty,NotifyPropertyChangedFor(nameof(MilkOrWool))] private string selectedType;
    [ObservableProperty] private string minWeight;
    [ObservableProperty] private string maxWeight;
    [ObservableProperty] private string minExpense;
    [ObservableProperty] private string maxExpense;
    [ObservableProperty] private string color;
    [ObservableProperty] private string minproduct;
    [ObservableProperty] private string maxproduct;
    public string MilkOrWool { get => (SelectedType == nameof(Cow) ? "Milk" : "Wool"); }

    public List<string> Types = ["All",nameof(Cow), nameof(Sheep)];

    [RelayCommand]
    async Task ReturnToMenu()
    {
        await Shell.Current.GoToAsync("..",true);
    }
}
