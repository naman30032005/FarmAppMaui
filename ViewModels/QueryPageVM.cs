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

    private readonly List<AnimalVM> animals;

    public QueryPageVM(LivestockPageVM vm)
    {
        Types = ["All", nameof(Cow), nameof(Sheep)];
        SelectedType = Types.First();
        animals = vm.animals.ToList();
    }

    [RelayCommand]
    async Task ReturnToMenu()
    {
        IsBusy = true;
        await Shell.Current.GoToAsync("..");
        IsBusy = false;
    }

    [RelayCommand]
    async Task Statistics()
    {
        var filteredList = GetFilteredAnimals().ToList();

        int count = filteredList.Count;
        int totalCount = animals.Count;

        double percentage = (totalCount > 0) ? (count * 100.0 / totalCount) : 0;
        double avgWeight = filteredList.Any() ? filteredList.Average(x => x.Weight) : 0;

        double dailyTax = filteredList.Sum(x => x.Weight * Calculator.GovernmentTax);

        double profitOrLoss = Calculator.IncomePerDay(filteredList) - Calculator.ExpensePerDay(filteredList);

        string profitOrLossToDisplay = (profitOrLoss > 0)?"Profit":"Loss";


        double produce = filteredList.Sum(x =>
           x.AnimalType == nameof(Cow) ? x.Milk :
           (x.AnimalType == nameof(Sheep) ? x.Wool : 0));


        string message = $"{"Number of Livestocks:",-35}{count,5}\n\n" +
            $"{"Percentage of Livestock:",-35}{percentage,7:F1} %\n\n" + 
            $"{"Daily Tax:",-35}{dailyTax,17:F2} $\n\n" +
            $"{profitOrLossToDisplay+":",-35}{profitOrLoss,20:F1} $\n\n" + 
            $"{"Average Weight:",-35}{avgWeight,11:F1} KG\n\n" + 
            $"{"Produce Amount:",-35}{produce,10:F1} KG";

        System.Diagnostics.Debug.WriteLine($"Income: {Calculator.IncomePerDay(filteredList)}");
        System.Diagnostics.Debug.WriteLine($"Expenses: {Calculator.ExpensePerDay(filteredList)}");
        System.Diagnostics.Debug.WriteLine($"Profit/Loss: {profitOrLoss}");

        await Shell.Current.DisplayAlert(
            "Statistics",
            message,
            "OK");
    }

    private IEnumerable<AnimalVM> GetFilteredAnimals()
    {
        IEnumerable<AnimalVM> filtered = animals;

        if (!string.IsNullOrEmpty(SelectedType) && !(SelectedType == "All"))
            filtered = filtered.Where(x => x.AnimalType == SelectedType);

        if (!string.IsNullOrEmpty(MinWeight))
        {
            float minW = Utils.ConvertInputFloat(MinWeight);
            if (minW != double.MinValue)
                filtered = filtered.Where(x => x.Weight >= minW);
        }

        if (!string.IsNullOrEmpty(MaxWeight))
        {
            float maxW = Utils.ConvertInputFloat(MaxWeight);
            if (maxW != double.MinValue)
                filtered = filtered.Where(x => x.Weight <= maxW);
        }

        if (!string.IsNullOrEmpty(Color))
        {
            string col = Utils.ConvertInputColor(Color).Trim().ToLowerInvariant();
            if (!string.IsNullOrEmpty(col))
                filtered = filtered.Where(x => Utils.ConvertInputColor(x.Colour).Trim().ToLowerInvariant() == col);
        }

        if (!string.IsNullOrEmpty(MinExpense))
        {
            float minE = Utils.ConvertInputFloat(MinExpense);
            if (minE != double.MinValue)
                filtered = filtered.Where(x => x.Expense >= minE);
        }

        if (!string.IsNullOrEmpty(MaxExpense))
        {
            float maxE = Utils.ConvertInputFloat(MaxExpense);
            if (maxE != double.MinValue)
                filtered = filtered.Where(x => x.Expense <= maxE);
        }

        if (!string.IsNullOrEmpty(Minproduct))
        {
            float minP = Utils.ConvertInputFloat(Minproduct);
            if (minP != double.MinValue)
            {
                if (SelectedType == nameof(Cow)) filtered = filtered.Where(x => x.Milk >= minP);
                else if (SelectedType == nameof(Sheep)) filtered = filtered.Where(x => x.Wool >= minP);
                else filtered = filtered.Where(x =>
                    (x.AnimalType == nameof(Cow) && x.Milk >= minP) ||
                    (x.AnimalType == nameof(Sheep) && x.Wool >= minP));
            }
        }

        if (!string.IsNullOrEmpty(Maxproduct))
        {
            float maxP = Utils.ConvertInputFloat(Maxproduct);
            if (maxP != double.MinValue)
            {
                if (SelectedType == nameof(Cow)) filtered = filtered.Where(x => x.Milk <= maxP);
                else if (SelectedType == nameof(Sheep)) filtered = filtered.Where(x => x.Wool <= maxP);
                else filtered = filtered.Where(x =>
                    (x.AnimalType == nameof(Cow) && x.Milk <= maxP) ||
                    (x.AnimalType == nameof(Sheep) && x.Wool <= maxP));
            }
        }

        return filtered;
    }
}
