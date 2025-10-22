using Microsoft.Maui.Controls;

namespace Farm.ViewModels;

public partial class ReportVM : BaseVM
{
    private readonly DbOps _db;
    List<AnimalVM> animals = new List<AnimalVM>();
    public ReportVM(DbOps dbs)
    {
        _db = dbs;
    }

    [ObservableProperty] private float tax;
    [ObservableProperty] private float avgWeight;
    [ObservableProperty,NotifyPropertyChangedFor(nameof(ProfitOrLoss))] private float porl;//profit or loss

    public string ProfitOrLoss => (porl > 0) ? "Daily Profit: " : "Daily Loss: ";

    public async Task CalculateFields()
    {
        if (animals.Count == 0) await ReadData();
        var avg = animals.Average(x => x.Weight);
        var tax = animals.Sum(x => x.Weight) * Calculator.GovernmentTax * 30;
        var porl = Calculator.IncomePerDay(animals) - Calculator.ExpensePerDay(animals);

        MainThread.BeginInvokeOnMainThread(() =>
        {
            AvgWeight = (float)avg;
            Tax = (float)tax;
            Porl = (float)porl;
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
}
