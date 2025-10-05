using Farm.Utitlity;

namespace Farm.ViewModels;

public partial class LivestockPageVM:BaseVM
{
    private readonly DbOps _db;
    public ObservableCollection<AnimalVM> animals { get; } = new();

    public List<String> SortOptions { get; }

    [ObservableProperty]
    private string selectedSortOption;

    public LivestockPageVM(DbOps dbs)
    {
        this._db = dbs;
        SortOptions = new() { "None", "ID ⬇️", "ID ⬆️"};
        SelectedSortOption = SortOptions[0];
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

