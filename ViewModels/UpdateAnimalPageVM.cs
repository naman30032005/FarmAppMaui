namespace Farm.ViewModels;

[QueryProperty("Animal", "AnimalVM")]
public partial class UpdateAnimalPageVM : BaseVM
{
    private readonly DbOps _db;
    [ObservableProperty] private AnimalVM animal;
    [ObservableProperty] private string milkOrWool;
    [ObservableProperty] private string milkOrWoolVal;

    public UpdateAnimalPageVM(DbOps dbs)
    {
        _db = dbs;
    }

    partial void OnAnimalChanged(AnimalVM value)
    {
        MilkOrWool = (Animal.AnimalType == nameof(Cow) ? "Milk" : "Wool");
        MilkOrWoolVal = Animal.MilkOrWoolValue;
    }

    [RelayCommand]
    async Task ReturnToMenu()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task Update()
    {

        var (isValid, errorMsg,morw) = CheckFields();

        if (!isValid)
        {
            await Shell.Current.DisplayAlert("Error", errorMsg, "OK");
            return;
        }
        else if (await Shell.Current.DisplayAlert("Confirmation","Are you sure you want to make changes to the item?","yes","no"))
        {

            IsBusy = true;

            if (Animal.AnimalType == nameof(Sheep)) Animal.Wool = morw;
            else Animal.Milk = morw;

            await _db.Update(Animal.animal);

            IsBusy = false;

            await ReturnToMenu();
        }
        else
        {
            return;
        }
    }

    private (bool isValid, string message,float morw) CheckFields()
    {
        float w = Utils.ConvertInputFloat(Animal.Weight.ToString());
        float e = Utils.ConvertInputFloat(Animal.Expense.ToString());
        float morw;
        if (MilkOrWool == "Milk") morw = Utils.ConvertInputFloat(MilkOrWoolVal);
        else morw = Utils.ConvertInputFloat(MilkOrWoolVal);
        string c = Utils.ConvertInputColor(Animal.Colour);

        if (w == float.MinValue)
            return (false, "Invalid weight entered.",0);
        if (e == float.MinValue)
            return (false, "Invalid expense entered.",0);
        if (morw == float.MinValue)
            return (false, $"Invalid {MilkOrWool} value entered.",0);
        if (string.IsNullOrEmpty(c))
            return (false, "Invalid colour entered.",0);

        return (true,string.Empty,morw);
    }
}
