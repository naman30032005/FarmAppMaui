namespace Farm.ViewModels;

public partial class DashboardVM : BaseVM
{
    private readonly DbOps _db;
    public List<AnimalVM> Animals = new List<AnimalVM>();

    public DashboardVM(DbOps dbs)
    {
        _db = dbs;
    }


    [ObservableProperty] private int totalAnimal;
    [ObservableProperty] private int totalCow;
    [ObservableProperty] private int totalSheep;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(ProfitorLoss))] private float prol;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(CowProfitDisplay)), NotifyPropertyChangedFor(nameof(SheepProfitDisplay))] private float cowProfit;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(SheepProfitDisplay)), NotifyPropertyChangedFor(nameof(CowProfitDisplay))] private float sheepProfit;

    public string CowProfitDisplay => $"Cow: {CowProfit:F1}$ ({(CowProfit > SheepProfit ? "More" : "Less")} Profitable)";
    public string SheepProfitDisplay => $"Sheep: {SheepProfit:F1}$ ({(SheepProfit > CowProfit ? "More" : "Less")} Profitable)";

    public string ProfitorLoss => (prol > 0) ? "Daily Profit " : "Daily Loss ";

    public async Task CalculateField()
    {
        if (Animals.Count == 0) await ReadData();
        else
        {
            Animals.Clear();
            await ReadData();
        }
        var prol = Calculator.IncomePerDay(Animals) - Calculator.ExpensePerDay(Animals);
        var avg = Animals.Average(x => x.Weight);
        var cowCount = Animals.Count(a => a.AnimalType == nameof(Cow));
        var sheepCount = Animals.Count(a => a.AnimalType == nameof(Sheep));
        var cp = (Calculator.IncomePerDay(Animals.Where(x => x.AnimalType == nameof(Cow)).ToList()) - Calculator.ExpensePerDay(Animals.Where(x => x.AnimalType == nameof(Cow)).ToList())) / Animals.Where(x => x.AnimalType == nameof(Cow)).Count();
        var sp = (Calculator.IncomePerDay(Animals.Where(x => x.AnimalType == nameof(Sheep)).ToList()) - Calculator.ExpensePerDay(Animals.Where(x => x.AnimalType == nameof(Sheep)).ToList())) / Animals.Where(x => x.AnimalType == nameof(Sheep)).Count(); ;

        MainThread.BeginInvokeOnMainThread(() =>
        {
            Prol = (float)prol;
            TotalAnimal = Animals.Count;
            CowProfit = cp;
            SheepProfit = sp;
            TotalCow = cowCount;
            TotalSheep = sheepCount;
        });
    }

    async Task ReadData()
    {
        IsBusy = true;

        Animals.Clear();

        var animalList = await _db.ReadDataAsync();
        foreach (var animal in animalList)
            Animals.Add(new AnimalVM(animal));

        IsBusy = false;
    }
}

