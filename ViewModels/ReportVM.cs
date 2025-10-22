using Microsoft.Maui.Controls;

namespace Farm.ViewModels;

public partial class ReportVM : BaseVM
{
    private readonly DbOps _db;
    public List<AnimalVM> animals = new List<AnimalVM>();
    public ReportVM(DbOps dbs)
    {
        _db = dbs;
    }

    [ObservableProperty] private float tax;
    [ObservableProperty] private float avgWeight;
    [ObservableProperty,NotifyPropertyChangedFor(nameof(ProfitOrLoss))] private float porl;//profit or loss
    [ObservableProperty,NotifyPropertyChangedFor(nameof(CowProfitDisplay)),NotifyPropertyChangedFor(nameof(SheepProfitDisplay))] private float cowProfit;
    [ObservableProperty,NotifyPropertyChangedFor(nameof(SheepProfitDisplay)),NotifyPropertyChangedFor(nameof(CowProfitDisplay))] private float sheepProfit;

    public string ProfitOrLoss => (porl > 0) ? "Daily Profit: " : "Daily Loss: ";
    public string CowProfitDisplay => $"Cow: {CowProfit:F1}$ ({(CowProfit > SheepProfit ? "More" : "Less")} Profitable)";
    public string SheepProfitDisplay => $"Sheep: {SheepProfit:F1}$ ({(SheepProfit > CowProfit ? "More" : "Less")} Profitable)";

    public async Task CalculateFields()
    {
        if (animals.Count == 0) await ReadData();
        else
        {
            animals.Clear();
            await ReadData();
        }
        var avg = animals.Average(x => x.Weight);
        var tax = animals.Sum(x => x.Weight) * Calculator.GovernmentTax * 30;
        var porl = Calculator.IncomePerDay(animals) - Calculator.ExpensePerDay(animals);
        var cp = (Calculator.IncomePerDay(animals.Where(x => x.AnimalType == nameof(Cow)).ToList()) - Calculator.ExpensePerDay(animals.Where(x => x.AnimalType == nameof(Cow)).ToList())) / animals.Where(x => x.AnimalType == nameof(Cow)).Count();
        var sp = (Calculator.IncomePerDay(animals.Where(x => x.AnimalType == nameof(Sheep)).ToList()) - Calculator.ExpensePerDay(animals.Where(x => x.AnimalType == nameof(Sheep)).ToList())) / animals.Where(x => x.AnimalType == nameof(Sheep)).Count(); ;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            AvgWeight = (float)avg;
            Tax = (float)tax;
            Porl = (float)porl;
            CowProfit = cp;
            SheepProfit = sp;
        });
    }

    async Task ReadData()
    {
        IsBusy = true;

        animals.Clear();

        var animalList = await _db.ReadDataAsync();
        foreach (var animal in animalList)
            animals.Add(new AnimalVM(animal));

        IsBusy = false;
    }

    [RelayCommand]
    async Task QueryAnimal()
    {
        await Shell.Current.GoToAsync($"{nameof(QueryPage)}", true);
    }

    [RelayCommand]
    async Task ForecastAnimal()
    {
        await Shell.Current.GoToAsync($"{nameof(Forecast)}", true);
    }
}
