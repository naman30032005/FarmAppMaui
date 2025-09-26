using Farm.Utitlity;

namespace Farm.ViewModels;

public class LivestockPageVM:BaseVM
{
    private readonly DbOps _db;
    public ObservableCollection<AnimalVM> animals { get; } = new();

    public LivestockPageVM(DbOps dbs)
    {
        this._db = dbs;
    }

    public async Task FillList()
    {
        if (IsBusy) return;
        IsBusy = true;

        animals.Clear();

        var animalList = await _db.ReadDataAsync();
        foreach (var animal in animalList)
            animals.Add(new AnimalVM(animal));

        IsBusy = false;
    }


}
