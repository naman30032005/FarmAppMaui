namespace Farm.ViewModels;

public partial class LivestockPageVM:BaseVM
{
    // For Databse and Items
    private readonly DbOps _db;
    public ObservableCollection<AnimalVM> animals { get; } = new();


    // For Sorting Categories and Options
    public List<String> SortOptions { get; }
    [ObservableProperty]
    private string selectedSortOption;

    [ObservableProperty,NotifyPropertyChangedFor(nameof(IsDescending))] private bool isAscending;
    private bool IsDescending = !;


    public LivestockPageVM(DbOps dbs)
    {
        this._db = dbs;
        SortOptions = new() { "None", "ID", "Weight", "Expense"};
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

    [RelayCommand]
    async Task AddAnimal()
    {
        await Shell.Current.GoToAsync($"{nameof(AddAnimalPage)}",true);
    }

    [RelayCommand]
    async Task UpdateAniamal()
    {
        await Shell.Current.GoToAsync("");
    }
    [RelayCommand]
    async Task DeleteAnimal()
    {
        await Shell.Current.GoToAsync("");
    }
    [RelayCommand]
    async Task QueryAnimal()
    {
        await Shell.Current.GoToAsync("");
    }

    [RelayCommand]
    async Task ClearFilter()
    {
        // To Do to implement a clear filter functionality;
    }

}
