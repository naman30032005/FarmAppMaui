using Farm.Utitlity;

namespace Farm.ViewModels;

public partial class AddAnimalPageVM:BaseVM
{
    private readonly DbOps _dbs;

    [ObservableProperty] private string animalType;
    [ObservableProperty] private float weight;
    [ObservableProperty] private string color;
    [ObservableProperty] private float expense;
    [ObservableProperty] private float milkOrWool;

    public AddAnimalPageVM(DbOps dbs)
    {
        _dbs = dbs;
    }

    [RelayCommand]
    public async void ReturnToMenu()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public async void SaveCommand()
    {
        if (AnimalType == nameof(Sheep))
        {
            Sheep sheep = new()
            {
                Weight = Weight,
                Expense = Expense,
                Colour = Color,
                Wool = MilkOrWool
            };

            await _dbs.Insert(sheep);
        }
        else if (AnimalType == nameof(Cow))
        {
            Cow cow = new()
            {
                Weight = Weight,
                Expense = Expense,
                Colour = Color,
                Milk = MilkOrWool
            };

            await _dbs.Insert(cow);
        }
    }
}
