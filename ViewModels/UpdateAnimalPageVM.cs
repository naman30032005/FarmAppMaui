namespace Farm.ViewModels;

[QueryProperty("AnimalVM", "AnimalVM")]
public class UpdateAnimalPageVM:BaseVM
{
    private readonly DbOps _db;
    [ObservableProperty] private AnimalVM animal;

    public UpdateAnimalPageVM(DbOps dbs)
    {
        _db = dbs;
    }
}
