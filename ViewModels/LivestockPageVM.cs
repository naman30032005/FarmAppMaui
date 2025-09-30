namespace Farm.ViewModels;

public partial class LivestockPageVM:BaseVM
{
    // For Databse and Items
    private readonly DbOps _db;
    public ObservableCollection<AnimalVM> animals { get; } = new();


    // For Sorting Categories and Options
    public List<String> SortOptions { get; }

    [ObservableProperty] private string selectedSortOption;



    [ObservableProperty] private AnimalVM selectedAnimal;
    [ObservableProperty] private bool deleteCommandEnabled;
    [ObservableProperty] private bool updateCommandEnabled;


    public LivestockPageVM(DbOps dbs)
    {
        this._db = dbs;
        SortOptions = new() { "None", "ID", "Weight", "Expense"};
        SelectedSortOption = SortOptions[0];
        DeleteCommandEnabled = false;
        UpdateCommandEnabled = false;
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
    async Task UpdateAnimal()
    {
        await Shell.Current.GoToAsync($"{nameof(UpdateAnimalPage)}", true,
            new Dictionary<string, object>{ 
                {"AnimalVM", SelectedAnimal} 
            });
    }
    [RelayCommand]
    async Task DeleteAnimal()
    {
        IsBusy = true;

        await Task.Delay(1000);

        await _db.Delete(SelectedAnimal.animal);

        animals.Remove(SelectedAnimal);

        DeleteCommandEnabled = false;

        IsBusy = false;
    }
    [RelayCommand]
    async Task QueryAnimal()
    {
        await Shell.Current.GoToAsync($"{nameof(QueryPage)}", true);
    }


    partial void OnSelectedAnimalChanged(AnimalVM value)
    {
        DeleteCommandEnabled = true;
    }


    [RelayCommand]
    async Task ClearFilter()
    {
        IsBusy = true;
        await FillList();
        this.SelectedSortOption = "None";
        IsBusy = false;
    }


    // For Sorting the List
    partial void OnSelectedSortOptionChanged(string value)
    {
        _ = ApplySorting();
    }

    private async Task ApplySorting()
    {
        if (animals == null || animals.Count == 0) return;

        IsBusy = true;

        await Task.Delay(1000); // For better UI

        await Task.Run(() =>
        {
            IEnumerable<AnimalVM> sorted = animals;

            switch (SelectedSortOption)
            {
                case "ID":
                    sorted = animals.OrderByDescending(a => a.ID);
                    break;

                case "Weight":
                    sorted = animals.OrderByDescending(a => a.Weight);
                    break;

                case "Expense":
                    sorted = animals.OrderByDescending(a => a.Expense);
                    break;

                case "None":
                default:
                    sorted = animals.OrderBy(a => a.ID);
                    break;
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                var sortedList = sorted.ToList();
                animals.Clear();
                foreach (var item in sortedList)
                    animals.Add(item);
            });
        });

        IsBusy = false;
    }
}
