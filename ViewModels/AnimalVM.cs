namespace Farm.ViewModels;

// Animal Vm For Auto Updating the Observable Collection When an animal is updated
public partial class AnimalVM : ObservableObject
{
    private readonly Animal animal;

    public AnimalVM(Animal animal)
    {
        this.animal = animal;

        expense = animal.Expense;
        weight = animal.Weight;
        colour = animal.Colour;

        // for derived classes
        if (animal is Cow cow) milk = cow.Milk; 
        if (animal is Sheep sheep) wool = sheep.Wool; 
    }

    public int IT => animal.ID;

    public string animalType => animal.AnimalType;

    [ObservableProperty]
    private float expense;
    
    [ObservableProperty]
    private float weight;
    
    [ObservableProperty]
    private string colour;

    [ObservableProperty]
    private float milk;

    [ObservableProperty]
    private float wool;

    // syncing back to the model;
    partial void OnExpenseChanged(float value) => animal.Expense = value;
    partial void OnWeightChanged(float value) => animal.Weight = value;
    partial void OnColourChanged(string value) => animal.Colour = value;
    partial void OnMilkChanged(float value) { if (animal is Cow cow) cow.Milk = value; }
    partial void OnWoolChanged(float value) { if (animal is Sheep sheep) sheep.Wool = value; }
}
