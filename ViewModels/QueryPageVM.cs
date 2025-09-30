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
    public string MilkOrWool { get => (SelectedType == "All") ? "Product" : (SelectedType == nameof(Cow) ? "Milk" : "Wool"); }

    public List<string> Types { get; set; }

    private ObservableCollection<AnimalVM> animals;

    public QueryPageVM(LivestockPageVM vm)
    {
        Types = ["All", nameof(Cow), nameof(Sheep)];
        SelectedType = Types[0];
        animals = vm.animals;
    }

    [RelayCommand]
    async Task ReturnToMenu()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("..");
        IsBusy = false;
    }

    [RelayCommand]
    async Task Apply()
    {
        return;
    }

    [RelayCommand]
    async Task Statistics()
    {
        return;
    }
}
