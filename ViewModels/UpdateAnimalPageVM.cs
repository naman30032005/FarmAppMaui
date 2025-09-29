namespace Farm.ViewModels;

[QueryProperty("Animal", "AnimalVM")]
public partial class UpdateAnimalPageVM : BaseVM
{
    private readonly DbOps _db;
    [ObservableProperty] private AnimalVM animal;

    public UpdateAnimalPageVM(DbOps dbs)
    {
        _db = dbs;
    }

    [RelayCommand]
    async Task ReturnToMenu()
    {
        await Shell.Current.GoToAsync("..");
    }
}
