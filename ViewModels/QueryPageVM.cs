namespace Farm.ViewModels;

public partial class QueryPageVM: BaseVM
{
    [ObservableProperty,NotifyPropertyChangedFor(nameof(MilkOrWool))] private string? selectedType;
    [ObservableProperty] private string? minWeight;
    [ObservableProperty] private string? maxWeight;
    [ObservableProperty] private string? minExpense;
    [ObservableProperty] private string? maxExpense;
    [ObservableProperty] private string? color;
    [ObservableProperty] private string? minproduct;
    [ObservableProperty] private string? maxproduct;
    public string MilkOrWool { get => (SelectedType == "All") ? "Product" : (SelectedType == nameof(Cow) ? "Milk" : "Wool"); }

    public List<string> Types { get; set; }

    private readonly ObservableCollection<AnimalVM> animals;

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
        await Task.Run(() =>
        {
            IEnumerable<AnimalVM> filtered = animals;

            if (!string.IsNullOrEmpty(SelectedType) && !(SelectedType == "All"))
            {
                filtered = filtered.Where(x => x.AnimalType == SelectedType);
            }

            if (!string.IsNullOrEmpty(MinWeight))
            {
                float minW = Utils.ConvertInputFloat(MinWeight);

                if (minW == Double.MinValue)
                {
                    Shell.Current.DisplayAlert("Error", "The Min Weight Field is Incorrectly added","Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Weight >= minW);
            }
            
            if (!string.IsNullOrEmpty(MaxWeight))
            {
                float maxW = Utils.ConvertInputFloat(MaxWeight);

                if (maxW == Double.MinValue)
                {
                    Shell.Current.DisplayAlert("Error", "The Max Weight Field is Incorrectly added","Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Weight <= maxW);
            }
            
            if (!string.IsNullOrEmpty(Color))
            {
                string col = Utils.ConvertInputColor(Color);

                if (col == string.Empty)
                {
                    Shell.Current.DisplayAlert("Error", "The Colour Field is Incorrectly added","Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Colour == col);
            }

            if (!string.IsNullOrEmpty(MinExpense))
            {
                float minE = Utils.ConvertInputFloat(MinExpense);

                if (minE == Double.MinValue)
                {
                    Shell.Current.DisplayAlert("Error", "The Min Expense Field is Incorrectly added", "Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Weight >= minE);
            }

            if (!string.IsNullOrEmpty(MaxExpense))
            {
                float maxE = Utils.ConvertInputFloat(MaxExpense);

                if (maxE == Double.MinValue)
                {
                    Shell.Current.DisplayAlert("Error", "The Max Expense Field is Incorrectly added","Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Weight <= maxE);
            }
            
            if (!string.IsNullOrEmpty(Minproduct))
            {
                float minP = Utils.ConvertInputFloat(Minproduct);

                if (minP == Double.MinValue)
                {
                    Shell.Current.DisplayAlert("Error", $"The Min {MilkOrWool} Field is Incorrectly added","Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Weight >= minP);
            }
            
            if (!string.IsNullOrEmpty(Maxproduct))
            {
                float maxP = Utils.ConvertInputFloat(Maxproduct);

                if (maxP == Double.MinValue)
                {
                    Shell.Current.DisplayAlert("Error", $"The Max {MilkOrWool} Field is Incorrectly added","Ok");
                    return;
                }

                filtered = filtered.Where(x => x.Weight <= maxP);
            }
        });
    }
}
