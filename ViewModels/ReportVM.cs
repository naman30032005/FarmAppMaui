namespace Farm.ViewModels;

public partial class ReportVM : BaseVM
{
    private readonly DbOps _db;
    public List<AnimalVM> animals = new List<AnimalVM>();
    public string[] TypeOptions { get => [nameof(Cow), nameof(Sheep)]; }
    [ObservableProperty] private string animalType;
    [ObservableProperty] private string value;
    [ObservableProperty] private float predictedPorl;
    [ObservableProperty] private string prediction;
    [ObservableProperty] private ChartEntry[] barChartEntries;
    [ObservableProperty] private ChartEntry[] donutChartEntries;
    [ObservableProperty] private Chart bCV; // bar chart
    [ObservableProperty] private Chart dCV; // Donut chart

    public ReportVM(DbOps dbs)
    {
        Prediction = "Profit";
        _db = dbs;
        AnimalType = nameof(Cow);
    }

    [ObservableProperty] private float tax;
    [ObservableProperty] private float avgWeight;
    [ObservableProperty,NotifyPropertyChangedFor(nameof(ProfitOrLoss))] private float porl;//profit or loss
    [ObservableProperty,NotifyPropertyChangedFor(nameof(CowProfitDisplay)),NotifyPropertyChangedFor(nameof(SheepProfitDisplay))] private float cowProfit;
    [ObservableProperty,NotifyPropertyChangedFor(nameof(SheepProfitDisplay)),NotifyPropertyChangedFor(nameof(CowProfitDisplay))] private float sheepProfit;

    public string ProfitOrLoss => (Porl > 0) ? "Daily Profit " : "Daily Loss ";
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
        var sp = (Calculator.IncomePerDay(animals.Where(x => x.AnimalType == nameof(Sheep)).ToList()) - Calculator.ExpensePerDay(animals.Where(x => x.AnimalType == nameof(Sheep)).ToList())) / animals.Where(x => x.AnimalType == nameof(Sheep)).Count();

        var camt = animals.Where(x => x.AnimalType == nameof(Cow)).Count();
        var samt = animals.Where(x => x.AnimalType == nameof(Sheep)).Count();

        var bce = new[] { new ChartEntry(camt) { Label = "Cow", Color = SKColor.Parse("#3498db"), ValueLabel = $"{camt}" },
                          new ChartEntry(samt) { Label = "Sheep", Color = SKColor.Parse("#77d065"), ValueLabel = $"{samt}"  } };

        var red = animals.Where(x => x.Colour.ToLowerInvariant() == "red").Count();
        var black = animals.Where(x => x.Colour.ToLowerInvariant() == "black").Count();
        var white = animals.Where(x => x.Colour.ToLowerInvariant() == "white").Count();

        var dce = new[] { new ChartEntry(red) { Label = "Red", Color = SKColor.Parse("#FF0000"), ValueLabel = $"{red}"},
                          new ChartEntry(black) { Label = "Black", Color = SKColor.Parse("#000000"), ValueLabel = $"{black}"},
                          new ChartEntry(white) { Label = "White", Color = SKColor.Parse("#FFFFFF"), ValueLabel = $"{white}"}
                        };

        MainThread.BeginInvokeOnMainThread(() =>
        {
            AvgWeight = (float)avg;
            Tax = (float)tax;
            Porl = (float)porl;
            CowProfit = cp;
            SheepProfit = sp;
            BarChartEntries = bce;
            DonutChartEntries = dce;
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
    async Task PredictProfit()
    {
        var val = Utils.ConvertInputFloat(Value);
        var profit = 0.0f;

        if (val == float.MinValue) { await Shell.Current.DisplayAlert("Error", "Please Enter a value before Predicting", "OK"); return; }

        if (AnimalType == nameof(Cow))
        {
            profit = CowProfit * val;
        }
        else if (AnimalType == nameof(Sheep))
        {
            profit = SheepProfit * val;
        }

        if (profit > 0) Prediction = "Profit";
        else Prediction = "Loss";

        PredictedPorl = profit;
        await Shell.Current.DisplayAlert("Prediction",$"Buying {val} {AnimalType}s would bring in an estimated daily{Prediction}: ${profit:F1}", "Ok");
    }

    partial void OnBarChartEntriesChanged(ChartEntry[] value)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            BCV = new BarChart { Entries = BarChartEntries ?? Array.Empty<ChartEntry>(),
                                 BackgroundColor = SKColor.Parse("#DDDDDD"),
                                 LabelTextSize = 32,
                                 Margin = 30,
                                 LabelOrientation = Orientation.Horizontal,
                                 ValueLabelOrientation = Orientation.Horizontal,
                                 MaxValue = 10,
                                 MinValue = 0,
            }; 
        });
    }
    
    partial void OnDonutChartEntriesChanged(ChartEntry[] value)
    {
        MainThread.BeginInvokeOnMainThread(() => {
            DCV = new DonutChart { Entries = DonutChartEntries ?? Array.Empty<ChartEntry>(),
                                 BackgroundColor = SKColor.Parse("#DDDDDD"),
                                 LabelTextSize = 32,
                                 Margin = 30
            }; 
        });
    }

    [RelayCommand]
    void Clear()
    {
        Value = string.Empty;
        PredictedPorl = 0.0f;
    }
}
