namespace Farm.ViewModels;

[QueryProperty("Animal", "AnimalVM")]
public partial class UpdateAnimalPageVM : BaseVM
{
    private readonly DbOps _db;
    [ObservableProperty] private AnimalVM animal;
    [ObservableProperty] private string milkOrWool;

    public UpdateAnimalPageVM(DbOps dbs)
    {
        _db = dbs;
    }

    partial void OnAnimalChanged(AnimalVM value)
    {
        MilkOrWool = (Animal.AnimalType == nameof(Cow) ? "Milk" : "Wool");
    }

    [RelayCommand]
    async Task ReturnToMenu()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Update()
    {

        var (isValid, errorMsg) = CheckFields();

        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error", errorMsg, "OK");
            return;
        }
        else if (await Shell.Current.DisplayAlert("Confirmation","Are you sure you want to make changes to the item?","yes","no"))
        {

            IsBusy = true;

            await _db.Update(Animal.animal);

            IsBusy = false;

            await ReturnToMenu();
        }
        else
        {
            return;
        }
    }

    private (bool isValid, string message) CheckFields()
    {
        float w = Utils.ConvertInputFloat(Animal.Weight.ToString());
        float e = Utils.ConvertInputFloat(Animal.Expense.ToString());
        float morw;
        if (MilkOrWool == "Milk") morw = Utils.ConvertInputFloat(Animal.Milk.ToString());
        else morw = Utils.ConvertInputFloat(Animal.Wool.ToString());
        string c = Utils.ConvertInputColor(Animal.Colour);

        if (w == float.MinValue)
            return (false, "Invalid weight entered.");
        if (e == float.MinValue)
            return (false, "Invalid expense entered.");
        if (morw == float.MinValue)
            return (false, $"Invalid {MilkOrWool} value entered.");
        if (string.IsNullOrEmpty(c))
            return (false, "Invalid colour entered.");

        return (true,string.Empty);
    }
}
