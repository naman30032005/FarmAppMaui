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

    }
}
