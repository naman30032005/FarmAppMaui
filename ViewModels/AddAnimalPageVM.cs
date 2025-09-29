namespace Farm.ViewModels;

public partial class AddAnimalPageVM : BaseVM
{
    private readonly DbOps _dbs;

    [ObservableProperty][NotifyPropertyChangedFor(nameof(ProductToDisplay))] private string animalType;
    [ObservableProperty] private string weight;
    [ObservableProperty] private string color;
    [ObservableProperty] private string expense;
    [ObservableProperty] private string milkOrWool;


    public string[] TypeOptions { get => [nameof(Cow), nameof(Sheep)]; } 
    public string ProductToDisplay { get => (AnimalType == nameof(Sheep)) ? "Wool" : "Milk"; }
    public AddAnimalPageVM(DbOps dbs)
    {
        _dbs = dbs;
        
    }

    [RelayCommand]
    public static async Task ReturnToMenu()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async Task Save()
    {

        var (isValid, errorMsg, w, e, m, c) = CheckFields();

        if (!isValid)
        {
            await Application.Current.MainPage.DisplayAlert("Error", errorMsg, "OK");
            return;
        }


        if (AnimalType == nameof(Sheep))
        {
            Sheep sheep = new()
            {
                Weight = w,
                Expense = e,
                Colour = c,
                Wool = m
            };

            await _dbs.Insert(sheep);
        }
        else if (AnimalType == nameof(Cow))
        {
            Cow cow = new()
            {
                Weight = w,
                Expense = e,
                Colour = c,
                Milk = m
            };

            await _dbs.Insert(cow);
        }
    }

    public (bool isValid, string errorMsg, float weight, float expense, float product, string colour) CheckFields()
    {
        float w = Utils.ConvertInputFloat(Weight);
        float e = Utils.ConvertInputFloat(Expense);
        float m = Utils.ConvertInputFloat(MilkOrWool);
        string c = Utils.ConvertInputColor(Color);

        if (w == float.MinValue)
            return (false, "Invalid weight entered.", 0, 0, 0, "");
        if (e == float.MinValue)
            return (false, "Invalid expense entered.", 0, 0, 0, "");
        if (m == float.MinValue)
            return (false, $"Invalid {ProductToDisplay} entered.", 0, 0, 0, "");
        if (string.IsNullOrWhiteSpace(c))
            return (false, "Invalid colour entered.", 0, 0, 0, "");

        return (true, string.Empty, w, e, m, c);
    }
}
